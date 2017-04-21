using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using Newtonsoft.Json;
using RestSharp;

namespace MyTasks.AndroidApp
{
    [Activity(Label = "MyTasks.AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ListView _tasksListView;
        private List<Task> _tasks = new List<Task>();
        private TextView _textView;
        private Button _addTaskButton;
        private TasksAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _addTaskButton = FindViewById<Button>(Resource.Id.addTaskButton);
            _textView = FindViewById<TextView>(Resource.Id.editText1);
            _tasksListView = FindViewById<ListView>(Resource.Id.taskListView);

            _adapter = new TasksAdapter(this, _tasks);
            _tasksListView.Adapter = _adapter;
            _tasksListView.ItemClick += OnListItemClick;

            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);

            GetTasks();

            _addTaskButton.Click += (sender, e) =>
            {
                var newTaskItem = new Task
                {
                    Id = new Guid(),
                    Title = _textView.Text == "" ? "New Task" : _textView.Text,
                    IsCompleted = false
                };

                _textView.Text = string.Empty;

                AddTask(newTaskItem);
                GetTasks();

                //Hide keyboard when the add taks button is clicked.
                imm.HideSoftInputFromWindow(_tasksListView.WindowToken, 0);

                //Switch focus to tasks list view.
                _tasksListView.RequestFocus();
            };
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var t = _tasks[e.Position];

            Toast.MakeText(this, t.Title, ToastLength.Short).Show();

            if (!t.IsCompleted)
            {
                MarkCompleted(t.Id);
            }
        }

        public void AddTask(Task item)
        {
            var client = new RestClient("http://api-azure-bootcamp-auckland.azurewebsites.net/api/Task");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(item);
            client.ExecuteAsync(request, response => {
                Console.WriteLine(response);
            });
        }

        public void MarkCompleted(Guid taskId)
        {
            var client = new RestClient("http://api-azure-bootcamp-auckland.azurewebsites.net/api/Task");
            var request = new RestRequest("/" + taskId, Method.PUT);

            client.ExecuteAsync(request, response => {
                GetTasks();
            });
        }

        public void GetTasks()
        {
            var client = new RestClient("http://api-azure-bootcamp-auckland.azurewebsites.net/api/Task");
            var request = new RestRequest(Method.GET);
            client.ExecuteAsync(request, response => {

                _tasks = JsonConvert.DeserializeObject<List<Task>>(response.Content);

                _adapter.UpdateTasks(_tasks);
                RunOnUiThread(() => _adapter.NotifyDataSetChanged());

            });
        }
    }
}
