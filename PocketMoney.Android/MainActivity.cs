using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;

using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;

namespace PocketMoney.Android
{
    [Activity(Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout _drawerLayout;
        private SupportToolbar _toolBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            
            _toolBar = FindViewById<SupportToolbar>(Resource.Id.toolBar);
            SetSupportActionBar(_toolBar);

            SupportActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_action_dashboard);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            SetUpDrawer();
            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.home:
                    _drawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetUpDrawer()
        {

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var drawerToggle = new ActionBarDrawerToggle(this, _drawerLayout, _toolBar, Resource.String.open_drawer, Resource.String.close_drawer);
            _drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            var navidationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            navidationView.NavigationItemSelected += (sender, e) =>
            {
                switch (e.MenuItem.ItemId)
                {
                    case (Resource.Id.nav_home):
                        Toast.MakeText(this, "Home menu selected", ToastLength.Short).Show();
                        DefaultBehavior(e);
                        break;
                    case (Resource.Id.nav_entries):
                        Toast.MakeText(this, "Entries menu selected", ToastLength.Short).Show();
                        break;
                }

                void DefaultBehavior(NavigationView.NavigationItemSelectedEventArgs eventArgs)
                {
                    eventArgs.MenuItem.SetChecked(true);
                    _drawerLayout.CloseDrawers();
                }
                
            };
            

        }

    }
}

