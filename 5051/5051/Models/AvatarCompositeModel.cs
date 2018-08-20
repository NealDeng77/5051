
namespace _5051.Models
{
    public class AvatarCompositeModel
    {
        public string AvatarHeadUri;
        public string AvatarAccessoryUri;
        public string AvatarCheeksUri;
        public string AvatarExpressionUri;
        public string AvatarHairFrontUri;
        public string AvatarHairBackUri;
        public string AvatarShirtFullUri;
        public string AvatarShirtShortUri;
        public string AvatarPantsUri;

        public string HeadUri;
        public string AccessoryUri;
        public string CheeksUri;
        public string ExpressionUri;
        public string HairFrontUri;
        public string HairBackUri;
        public string ShirtFullUri;
        public string ShirtShortUri;
        public string PantsUri;

        public string HeadId;
        public string ShirtShortId;
        public string ShirtFullId;
        public string AccessoryId;
        public string CheeksId;
        public string ExpressionId;
        public string HairFrontId;
        public string HairBackId;
        public string PantsId;

        public string AvatarBase = "/Content/Avatar/";

        public AvatarCompositeModel()
        {
            var item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Head);
            HeadId = item.Id;
            HeadUri = item.Uri;
            AvatarHeadUri = AvatarBase + HeadUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.ShirtShort);
            ShirtShortId = item.Id;
            ShirtShortUri = item.Uri;
            AvatarShirtShortUri = AvatarBase + ShirtShortUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.ShirtFull);
            ShirtFullId = item.Id;
            ShirtFullUri = item.Uri;
            AvatarShirtFullUri = AvatarBase + ShirtFullUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Accessory);
            AccessoryId = item.Id;
            AccessoryUri = item.Uri;
            AvatarAccessoryUri = AvatarBase + AccessoryUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Cheeks);
            CheeksId = item.Id;
            CheeksUri = item.Uri;
            AvatarCheeksUri = AvatarBase + CheeksUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Expression);
            ExpressionId = item.Id;
            ExpressionUri = item.Uri;
            AvatarExpressionUri = AvatarBase + ExpressionUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.HairFront);
            HairFrontId = item.Id;
            HairFrontUri = item.Uri;
            AvatarHairFrontUri = AvatarBase + HairFrontUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.HairBack);
            HairBackId = item.Id;
            HairBackUri = item.Uri;
            AvatarHairBackUri = AvatarBase + HairBackUri;

            item = Backend.DataSourceBackend.Instance.AvatarItemBackend.GetDefault(AvatarItemCategoryEnum.Pants);
            PantsId = item.Id;
            PantsUri = item.Uri;
            AvatarPantsUri = AvatarBase + PantsUri;
        }
    }
}