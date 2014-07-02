namespace Maxwe11.Bimap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Maxwe11.Bimap.Attributes;

    /// <summary>
    /// Provides a means of reading a sequence of objects from byte array
    /// </summary>
    public sealed class ObjectReader
    {
        #region Fields

        private static readonly MethodInfo CreatePropertyMethod = typeof(Property).GetMethod("Create", BindingFlags.Static | BindingFlags.Public);

        private static readonly Dictionary<Type, IEnumerable<Property>> Cache = new Dictionary<Type, IEnumerable<Property>>();

        private readonly Type mTargetType;

        private readonly IEnumerable<Property> mProperties;

        private readonly BitsReader mBitsReader;

        #endregion

        #region Constructor

        private ObjectReader(Type targetType, IEnumerable<Property> properties, BitsReader reader)
        {
            mTargetType = targetType;
            mProperties = properties;
            mBitsReader = reader;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the target type of <see cref="ObjectReader"/>
        /// </summary>
        public Type TargetType { get { return mTargetType; } }

        /// <summary>
        /// Gets the current bit-position within the byte array
        /// </summary>
        public int BitsRead { get { return mBitsReader.BitsRead; } }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new <see cref="ObjectReader"/> instance for reading the supplied data
        /// </summary>
        /// <typeparam name="T">The type of objects for reading</typeparam>
        /// <param name="bytes">The array of bytes from which to read objects</param>
        /// <param name="cache"></param>
        /// <returns>An object that is used to read objects from byte array</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>For now target type <typeparamref name="T"/> should be a POCO class</remarks>
        public static ObjectReader Create<T>(byte[] bytes, bool cache = true) where T : class
        {
            var type = typeof(T);
            IEnumerable<Property> props;

            if (Cache.TryGetValue(type, out props) == false)
            {
                props = GetTypeProperties(type);

                if (cache)
                    Cache.Add(type, props);
            }

            var reader = new BitsReader(bytes);

            return new ObjectReader(type, props, reader);
        }

        private static IEnumerable<Property> GetTypeProperties(Type type)
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (props.Length == 0)
            {
                var msg = "Target type \'" + type.Name + "\' has no public instance properties";
                throw new ArgumentException(msg);
            }

            var propsInfo = props
                            .Select(x => new { Property = x, attr = x.GetCustomAttributes(typeof(BimapAttribute), false) })
                            .Where(x => x.attr.Length != 0)
                            .Select(x => new { x.Property, Map = (BimapAttribute)x.attr[0]})
                            .OrderBy(x => x.Map.OrderId)
                            .ToArray();

            if (propsInfo.Length == 0)
            {
                var msg = "Target type \'" + type.Name + "\' has no properties with \'BimapAttribute\' attribute";
                throw new ArgumentException(msg);
            }

            var mappedProperties = new List<Property>(propsInfo.Length);

            for (int i = 0; i < propsInfo.Length; i++)
            {
                var propInfo = propsInfo[i];
                var prop = propInfo.Property;
                var last = mappedProperties.LastOrDefault();

                if (i > 0  && propInfo.Map.OrderId == propsInfo[i - 1].Map.OrderId)
                {
                    var msg = string.Format("Properties \'{0}\' and \'{1}\' has same order id", last.Name, prop.Name);
                    throw new ArgumentException(msg);
                }

                var setter = prop.GetSetMethod();

                if (setter == null)
                {
                    var msg = string.Format("Property \'{0}\' that you want to map has no accessible setter", prop.Name);
                    throw new ArgumentException(msg);
                }

                var bitsCount = propInfo.Map.BitsCount;

                try
                {
                    var creator = CreatePropertyMethod.MakeGenericMethod(prop.DeclaringType, prop.PropertyType);
                    var property = (Property)creator.Invoke(null, new object[] { prop.Name, setter, bitsCount });
                    mappedProperties.Add(property);
                }
                catch (TargetInvocationException e)
                {
                    throw e.InnerException;
                }
            }

            return mappedProperties;
        }

        /// <summary>
        /// Populates all properties marked with <see cref="Bimap.Attributes.BimapAttribute"/>
        /// </summary>
        /// <param name="object">Object for reading</param>
        /// <remarks><see cref="object"/> should have the same type as target type of <see cref="ObjectReader"/></remarks>
        public void Read(object @object)
        {
            var visitor = new NumbersReaderVisitor(mBitsReader);
            foreach (var property in mProperties)
            {
                property.Apply(@object, visitor);
            }
        }

        /// <summary>
        /// Sets new byte array for reading objects
        /// </summary>
        /// <param name="bytes">The array of unsigned bytes from which to read objects</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Reset(byte[] bytes)
        {
            mBitsReader.Reset(bytes);
        }

        #endregion
    }
}
