using System;
using System.Collections.Generic;
using Microsoft.Graph;
using Xamarin.Forms;

namespace GraphTutorial
{
    public partial class MeetingDetailsPage : ContentPage
    {
        private Event SelectedEvent { get; set; }

        public MeetingDetailsPage(Event selectedEvent)
        {
            InitializeComponent();
            this.meetingName.Text = selectedEvent.Subject;
            this.startDate.Text = selectedEvent.Start.DateTime;
            this.listOfAttendees.ItemsSource = selectedEvent.Attendees;
        }

    }
}
