namespace BKvs2010.Usercontrols
{
    public class FavoriteTextBoxForEyeExam : PrototypeFavoriteTextBoxForEyeExam
    {
        public FavoriteTextBoxForEyeExam()
        {
            base.ButtonPosition = PrototypeFavoriteTextBoxButtonPositionForEyeExam.TopRight;
        }

        public string FavoriteOrder { get; set; }
        public string FavoriteType { get; set; }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FavoriteTextBoxForEyeExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "FavoriteTextBoxForEyeExam";
            this.Size = new System.Drawing.Size(244, 43);
            this.ResumeLayout(false);

        }

        public int SelectedIndex { get; set; }




    }
}