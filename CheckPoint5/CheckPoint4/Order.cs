//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace CheckPoint4
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Customer))]
    [KnownType(typeof(OrderItem))]
    [KnownType(typeof(Manager))]
    public partial class Order: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int order_id
        {
            get { return _order_id; }
            set
            {
                if (_order_id != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'order_id' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _order_id = value;
                    OnPropertyChanged("order_id");
                }
            }
        }
        private int _order_id;
    
        [DataMember]
        public Nullable<System.DateTime> date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged("date");
                }
            }
        }
        private Nullable<System.DateTime> _date;
    
        [DataMember]
        public Nullable<System.TimeSpan> time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged("time");
                }
            }
        }
        private Nullable<System.TimeSpan> _time;
    
        [DataMember]
        public Nullable<int> customer_id
        {
            get { return _customer_id; }
            set
            {
                if (_customer_id != value)
                {
                    ChangeTracker.RecordOriginalValue("customer_id", _customer_id);
                    if (!IsDeserializing)
                    {
                        if (Customer != null && Customer.customer_id != value)
                        {
                            Customer = null;
                        }
                    }
                    _customer_id = value;
                    OnPropertyChanged("customer_id");
                }
            }
        }
        private Nullable<int> _customer_id;
    
        [DataMember]
        public Nullable<int> manager_id
        {
            get { return _manager_id; }
            set
            {
                if (_manager_id != value)
                {
                    ChangeTracker.RecordOriginalValue("manager_id", _manager_id);
                    if (!IsDeserializing)
                    {
                        if (Manager != null && Manager.manager_id != value)
                        {
                            Manager = null;
                        }
                    }
                    _manager_id = value;
                    OnPropertyChanged("manager_id");
                }
            }
        }
        private Nullable<int> _manager_id;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Customer Customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = value;
                    FixupCustomer(previousValue);
                    OnNavigationPropertyChanged("Customer");
                }
            }
        }
        private Customer _customer;
    
        [DataMember]
        public TrackableCollection<OrderItem> OrderItems
        {
            get
            {
                if (_orderItems == null)
                {
                    _orderItems = new TrackableCollection<OrderItem>();
                    _orderItems.CollectionChanged += FixupOrderItems;
                }
                return _orderItems;
            }
            set
            {
                if (!ReferenceEquals(_orderItems, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_orderItems != null)
                    {
                        _orderItems.CollectionChanged -= FixupOrderItems;
                        // This is the principal end in an association that performs cascade deletes.
                        // Remove the cascade delete event handler for any entities in the current collection.
                        foreach (OrderItem item in _orderItems)
                        {
                            ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                        }
                    }
                    _orderItems = value;
                    if (_orderItems != null)
                    {
                        _orderItems.CollectionChanged += FixupOrderItems;
                        // This is the principal end in an association that performs cascade deletes.
                        // Add the cascade delete event handler for any entities that are already in the new collection.
                        foreach (OrderItem item in _orderItems)
                        {
                            ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                        }
                    }
                    OnNavigationPropertyChanged("OrderItems");
                }
            }
        }
        private TrackableCollection<OrderItem> _orderItems;
    
        [DataMember]
        public Manager Manager
        {
            get { return _manager; }
            set
            {
                if (!ReferenceEquals(_manager, value))
                {
                    var previousValue = _manager;
                    _manager = value;
                    FixupManager(previousValue);
                    OnNavigationPropertyChanged("Manager");
                }
            }
        }
        private Manager _manager;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            Customer = null;
            OrderItems.Clear();
            Manager = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupCustomer(Customer previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (Customer != null)
            {
                if (!Customer.Orders.Contains(this))
                {
                    Customer.Orders.Add(this);
                }
    
                customer_id = Customer.customer_id;
            }
            else if (!skipKeys)
            {
                customer_id = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Customer")
                    && (ChangeTracker.OriginalValues["Customer"] == Customer))
                {
                    ChangeTracker.OriginalValues.Remove("Customer");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Customer", previousValue);
                }
                if (Customer != null && !Customer.ChangeTracker.ChangeTrackingEnabled)
                {
                    Customer.StartTracking();
                }
            }
        }
    
        private void FixupManager(Manager previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (Manager != null)
            {
                if (!Manager.Orders.Contains(this))
                {
                    Manager.Orders.Add(this);
                }
    
                manager_id = Manager.manager_id;
            }
            else if (!skipKeys)
            {
                manager_id = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Manager")
                    && (ChangeTracker.OriginalValues["Manager"] == Manager))
                {
                    ChangeTracker.OriginalValues.Remove("Manager");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Manager", previousValue);
                }
                if (Manager != null && !Manager.ChangeTracker.ChangeTrackingEnabled)
                {
                    Manager.StartTracking();
                }
            }
        }
    
        private void FixupOrderItems(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (OrderItem item in e.NewItems)
                {
                    item.Order = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("OrderItems", item);
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Update the event listener to refer to the new dependent.
                    ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OrderItem item in e.OldItems)
                {
                    if (ReferenceEquals(item.Order, this))
                    {
                        item.Order = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("OrderItems", item);
                        // Delete the dependent end of this identifying association. If the current state is Added,
                        // allow the relationship to be changed without causing the dependent to be deleted.
                        if (item.ChangeTracker.State != ObjectState.Added)
                        {
                            item.MarkAsDeleted();
                        }
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Remove the previous dependent from the event listener.
                    ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                }
            }
        }

        #endregion
    }
}
