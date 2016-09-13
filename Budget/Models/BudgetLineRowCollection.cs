using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Models
{
    public class BudgetLineRowCollection : List<BudgetLine>, ITypedList, ICustomTypeDescriptor
    {
        private BudgetLineDataTable _parent;

        public BudgetLineRowCollection(BudgetLineDataTable parent)
        {
            _parent = parent;
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return _parent.Columns;
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return null;
        }

        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        public string GetClassName()
        {
            return typeof(BudgetLine).FullName;
        }

        public string GetComponentName()
        {
            return typeof(BudgetLine).Name;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetItemProperties(null);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetItemProperties(null);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return null;
        }
    }
}
