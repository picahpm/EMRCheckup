using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EMRQuestionnaire.clsQuestionaire
{
    public class clsOtherHealthIssuesfavoritefood : clsFamilyIllness
    {
        private char _RICE;

        public char RICE
        {
            get { return _RICE; }
            set { _RICE = value; }
        }
        private char _DEEP_FRIED_FOOD;

        public char DEEP_FRIED_FOOD
        {
            get { return _DEEP_FRIED_FOOD; }
            set { _DEEP_FRIED_FOOD = value; }
        }
        private char _FAST_FOOD;

        public char FAST_FOOD
        {
            get { return _FAST_FOOD; }
            set { _FAST_FOOD = value; }
        }
        private char _INSTANT_NOODLE;

        public char INSTANT_NOODLE
        {
            get { return _INSTANT_NOODLE; }
            set { _INSTANT_NOODLE = value; }
        }
        private char _VEGETABLE;

        public char VEGETABLE
        {
            get { return _VEGETABLE; }
            set { _VEGETABLE = value; }
        }
        private char _SNACK;

        public char SNACK
        {
            get { return _SNACK; }
            set { _SNACK = value; }
        }
        private char _FISH;

        public char FISH
        {
            get { return _FISH; }
            set { _FISH = value; }
        }
    }
}
