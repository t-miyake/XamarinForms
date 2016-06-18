using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace TaitoTourismMap
{
    public class EventToCommandBehavior : BindableBehavior<View>
    {

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create("EventName", typeof(string), typeof(EventToCommandBehavior), default(string),
                                                                                            propertyChanged: (bindable, oldValue, newValue) =>
                                                                                            ((EventToCommandBehavior)bindable).OnPropertyChanged(EventNameProperty.PropertyName));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior), default(ICommand),
                                                                                          propertyChanged: (bindable, oldValue, newValue) =>
                                                                                          ((EventToCommandBehavior)bindable).OnPropertyChanged(CommandProperty.PropertyName));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior), default(object),
                                                                                                   propertyChanged: (bindable, oldValue, newValue) =>
                                                                                                   ((EventToCommandBehavior)bindable).OnPropertyChanged(CommandParameterProperty.PropertyName));

        public static readonly BindableProperty EventArgsConverterProperty = BindableProperty.Create("EventArgsConverter", typeof(IValueConverter), typeof(EventToCommandBehavior), default(IValueConverter),
                                                                                                     propertyChanged: (bindable, oldValue, newValue) =>
                                                                                                     ((EventToCommandBehavior)bindable).OnPropertyChanged(EventArgsConverterProperty.PropertyName));

        public static readonly BindableProperty EventArgsConverterParameterProperty = BindableProperty.Create("EventArgsConverterParameter", typeof(object), typeof(EventToCommandBehavior), default(object),
                                                                                                              propertyChanged: (bindable, oldValue, newValue) =>
                                                                                                              ((EventToCommandBehavior)bindable).OnPropertyChanged(EventArgsConverterParameterProperty.PropertyName));

        private Delegate _handler;
        private EventInfo _eventInfo;

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public IValueConverter EventArgsConverter
        {
            get { return (IValueConverter)GetValue(EventArgsConverterProperty); }
            set { SetValue(EventArgsConverterProperty, value); }
        }

        public object EventArgsConverterParameter
        {
            get { return GetValue(EventArgsConverterParameterProperty); }
            set { SetValue(EventArgsConverterParameterProperty, value); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            var events = AssociatedObject.GetType().GetRuntimeEvents().ToArray();
            if (events.Any())
            {
                _eventInfo = events.FirstOrDefault(e => e.Name == EventName);
                if (_eventInfo == null)
                {
                    throw new ArgumentException(String.Format("EventToCommand: Can't find any event named '{0}' on attached type", EventName));
                }

                AddEventHandler(_eventInfo, AssociatedObject, OnFired);
            }
        }

        protected override void OnDetachingFrom(View bindable)
        {
            if (_handler != null)
            {
                _eventInfo.RemoveEventHandler(AssociatedObject, _handler);
            }

            base.OnDetachingFrom(bindable);
        }

        private void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            var eventParameters = eventInfo.EventHandlerType
                .GetRuntimeMethods().First(m => m.Name == "Invoke")
                .GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType))
                .ToArray();

            var actionInvoke = action.GetType()
                .GetRuntimeMethods().First(m => m.Name == "Invoke");

            _handler = Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action), actionInvoke, eventParameters[0], eventParameters[1]),
                eventParameters
            )
            .Compile();

            eventInfo.AddEventHandler(item, _handler);
        }

        private void OnFired(object sender, EventArgs eventArgs)
        {
            if (Command == null)
            {
                return;
            }

            var parameter = CommandParameter;

            if (eventArgs != null && eventArgs != EventArgs.Empty)
            {
                parameter = eventArgs;

                if (EventArgsConverter != null)
                {
                    parameter = EventArgsConverter.Convert(eventArgs, typeof(object), EventArgsConverterParameter, CultureInfo.CurrentUICulture);
                }
            }

            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }
    }
}