// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GraphTutorial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
        }

        private IUserEventsCollectionPage events;
        private List<Event> eventsList;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Get the events
            events = await App.GraphClient.Me.Events.Request()
                .Select(e => new 
                { 
                    e.Subject, 
                    e.Organizer, 
                    e.Start, 
                    e.End ,
                    e.Attendees,
                    e.BodyPreview,
                })
                .OrderBy("createdDateTime DESC")
                .GetAsync();

            // Add the events to the list view
            eventsList = events.CurrentPage.ToList();

            CalendarList.ItemsSource = eventsList; 
        }

        void CalendarList_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (CalendarList.SelectedItem != null)
            {
                var selectedEvent = (Event) CalendarList.SelectedItem; 

                var detailPage = new MeetingDetailsPage(selectedEvent);

                Navigation.PushAsync(detailPage);
            }
        }

        void DatePicker_DateSelected(System.Object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
           var answer =  eventsList.Where((singleEvent) => {

                var startdate = singleEvent.Start.DateTime.Substring(0, 10);

               Console.WriteLine("**->  "+startdate);
               var actualDate = DateTime.Parse(startdate);
                
               var answer1 =DateTime.Compare(actualDate,e.NewDate) == 0;

               Console.WriteLine(actualDate + " " + e.NewDate + " " + answer1);


               return answer1;

            });

            foreach (Event eve in answer)
            {
                Console.WriteLine(eve.Start);
            }

            CalendarList.ItemsSource = answer;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            CalendarList.ItemsSource = eventsList;
        }
    }
}