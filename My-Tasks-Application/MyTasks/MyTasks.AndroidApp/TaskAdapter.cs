using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace MyTasks.AndroidApp
{
    class TasksAdapter : BaseAdapter<Task>
    {
        List<Task> _items;
        private readonly Activity _context;

        public TasksAdapter(MainActivity context, List<Task> tasks)
        {
            _context = context;
            _items = tasks;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];

            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.list_item, null);

            var taskItemView = view.FindViewById<TextView>(Resource.Id.taskListItem);
            taskItemView.Text = item.Title;

            if (item.IsCompleted)
            {
                taskItemView.Text += " - Done";
            }

            return view;
        }

        public override int Count => _items.Count;

        public override Task this[int position]
        {
            get { return _items[position]; }
        }

        public void UpdateTasks(List<Task> tasks)
        {
            _items = new List<Task>();
            foreach (var task in tasks)
            {
                _items.Add(task);
            }
        }
    }
}