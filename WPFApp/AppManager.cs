using ContractLib;
using ContractLib.UserComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFApp.Controls.MenuControls;
using WPFApp.Controls.MenuControls.CatalogControls;
using WPFApp.Controls.MenuControls.InvitationsControls;
using WPFApp.Controls.MenuControls.TestEditControls;
using WPFApp.Controls.SignControls;

namespace WPFApp
{
    public class AppManager
    {
        public UserInfo User { get; set; }
        MainWindow mw;
        public MenuControl MenuControl { get; set; }
        public TestEditControl TestEditControl { get; set; }
        public QuestionEditControl QuestionEditControl { get; set; }
        public LoadImageControl LoadImageControl { get; set; }
        public string WindowTitle
        {
            get
            {
                return mw.Title;
            }
            set
            {
                mw.Title = value;
            }
        }

        #region CurControl

        UserControl curControl;
        public UserControl CurControl
        {
            get
            {
                return curControl;
            }
            set
            {
                if (curControl != null)
                    mw.RemoveControl(curControl);

                curControl = value;

                if (value != null)
                    mw.AddControl(value);
            }
        }
        #endregion
        #region CurMessageControl

        UserControl curMessageControl;
        public UserControl CurMessageControl
        {
            get
            {
                return curMessageControl;
            }
            set
            {
                if (curMessageControl != null)
                    mw.RemoveControl(curMessageControl);

                curMessageControl = value;

                if (value != null)
                    mw.AddControl(value);
            }
        }
        #endregion

        
        public AppManager(MainWindow mw)
        {
            Connect("net.tcp://localhost:4222/IContract");

            instance = this;
            this.mw = mw;
            
            CurControl = new SignInControl();
        }
        #region instance, Instanse

        private static AppManager instance;

        public static AppManager Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("Инициализируйте AppManager.Instance.");
                return instance;
            }
        }

        #endregion
        #region Channel, Connect(-)

        public IContract Channel { get; set; }

        void Connect(string uri)
        {
            Uri address = new Uri(uri);
            NetTcpBinding binding = new NetTcpBinding();
            binding.MaxReceivedMessageSize = int.MaxValue;
            EndpointAddress endpoint = new EndpointAddress(address);
            ChannelFactory<IContract> factory = new ChannelFactory<IContract>(binding, endpoint);
            Channel = factory.CreateChannel();
        }

        #endregion

        #region Sign, UpdateUser

        public void SignIn()
        {
            UpdateUser();
            CurControl = MenuControl = new MenuControl();

            Dictionary<string, UserControl> elements = new Dictionary<string, UserControl>();
            elements.Add("Тесты", new CatalogControl());
            elements.Add("Мои тесты", new CatalogControl(User.Id));
            elements.Add("Приглашения", new InvitationsControl());
            elements.Add("Профиль", new ProfileControl());

            MenuControl.Use(elements);
            MenuControl.Back();

            MenuControl.Updated += (elements["Тесты"] as CatalogControl).UpdatePagesNav;
            MenuControl.Updated += (elements["Приглашения"] as InvitationsControl).UpdatePagesNav;
            MenuControl.Updated += (elements["Мои тесты"] as CatalogControl).UpdatePagesNav;

            TestEditControl = new TestEditControl();
            QuestionEditControl = new QuestionEditControl();
        }

        public void SignOut(bool closeWindow)
        {
            Channel.SignOut();

            if (closeWindow)
                mw.Close();

            MenuControl = null;
            TestEditControl = null;
            CurMessageControl = null;
            User = null;

            CurControl = new SignInControl();
        }

        public void UpdateUser()
        {
            User = Channel.GetUser();
        }

        #endregion

        public static BitmapImage GetBitmapImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (var ms = new System.IO.MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
