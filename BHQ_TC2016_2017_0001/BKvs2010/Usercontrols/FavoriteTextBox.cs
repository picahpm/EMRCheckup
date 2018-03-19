namespace BKvs2010.Usercontrols
{
    public class FavoriteTextBox : PrototypeFavoriteTextBox
    {
        public FavoriteTextBox()
        {
            base.ButtonPosition = PrototypeFavoriteTextBoxButtonPosition.TopRight;
        }

        public string FavoriteOrder { get; set; }
        public string FavoriteType { get; set; }
    }
}
