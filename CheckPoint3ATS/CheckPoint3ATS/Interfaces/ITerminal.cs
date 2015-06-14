using System;

namespace CheckPoint3ATS
{
    public interface ITerminal
    {
        string Model { get; }
        int Cost { get; }
        int TerminalId { get; }
        event Func<ISubscriber, int, PortMode> DialingEvent;
        event EventHandler<EventArgsForTerminalEndCall> HangUpPhoneEvent;
        event Func<ISubscriber, PortMode> PickUpEvent;
        event EventHandler<EventArgs> DisconnectFromPortEvent;
        event EventHandler<EventArgs> ConnectFromPortEvent;
        void DisconnectFromPort(ISubscriber sender);
        void ConnectFromPort(ISubscriber sender);
        PortMode Dialing(ISubscriber sender, int phoneNumber);
        PortMode PickUp(ISubscriber sender);
        void HangUpPhone(object sender, EventArgsForTerminalEndCall args);
    }
}