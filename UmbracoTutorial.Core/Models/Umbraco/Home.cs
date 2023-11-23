namespace UmbracoTutorial.Core.UmbracoModels
{
    public partial class Home
    {
        public string ShortHeroDescription
        {
            get
            {
                if (string.IsNullOrEmpty(HeroDescription))
                {
                    return "";
                }
                return $"{HeroDescription.Substring(0, 30)}...";
            }


        }
    }
}

