using System;
using System.IO;
using System.Drawing;

using STA.Common;
using STA.Common.ImgHelper;
using STA.Config;

namespace STA.Core
{
    public enum AvatarSize { Original, Large, Medium, Small }
    public class Avatars
    {
        const string AVATAR_URL = "sta/avators/{0}/{1}/{2}/{3}_avatar_{4}.jpg";
        private static string sitePath = BaseConfigs.GetSitePath;
        /// <summary>
        /// 获取头像地址
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="avatarSize">头像大小，1：大，2：中，3：小</param>
        /// <returns></returns>
        public static string GetAvatarUrl(int uid, AvatarSize avatarSize)
        {
            string uidstr = FormatUid(uid);
            string physicsAvatarPath = string.Format(AVATAR_URL, uidstr.Substring(0, 3), uidstr.Substring(3, 2), uidstr.Substring(5, 2), uidstr.Substring(7, 2), avatarSize.ToString().ToLower());
            return Utils.GetRootUrl(sitePath + "/") + physicsAvatarPath;
        }

        /// <summary>
        /// 获取默认头像
        /// </summary>
        /// <param name="avatarSize"></param>
        /// <returns></returns>
        public static string GetDefaultAvatarUrl(AvatarSize avatarSize)
        {
            return Utils.GetRootUrl(sitePath + "/") + "sta/pics/avator/noavatar_" + avatarSize.ToString().ToLower() + ".gif";
        }

        /// <summary>
        /// 格式化Uid为9位标准格式
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string FormatUid(int uid)
        {
            return uid.ToString().PadLeft(9, '0');
        }

        /// <summary>
        /// 是否存在上传头像
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool ExistAvatar(int uid)
        {
            string uidstr = FormatUid(uid);

            return File.Exists(GetPhysicsAvatarPath(uidstr, AvatarSize.Original)) &&
                   File.Exists(GetPhysicsAvatarPath(uidstr, AvatarSize.Large)) &&
                   File.Exists(GetPhysicsAvatarPath(uidstr, AvatarSize.Medium)) &&
                   File.Exists(GetPhysicsAvatarPath(uidstr, AvatarSize.Small));
        }

        /// <summary>
        /// 获取头像物理路径
        /// </summary>
        /// <param name="uidstr"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string GetPhysicsAvatarPath(string uidstr, AvatarSize size)
        {
            return Utils.GetMapPath(sitePath + "/" +
                string.Format(AVATAR_URL, uidstr.Substring(0, 3), uidstr.Substring(3, 2), uidstr.Substring(5, 2), uidstr.Substring(7, 2), size.ToString().ToLower()));
        }

        /// <summary>
        /// 删除头像
        /// </summary>
        /// <param name="uid"></param>
        public static void DeleteAvatar(int uid)
        {
            string uidstr = FormatUid(uid);
            if (File.Exists(Avatars.GetPhysicsAvatarPath(uidstr, AvatarSize.Original)))
            {
                File.Delete(Avatars.GetPhysicsAvatarPath(uidstr, AvatarSize.Original));
                File.Delete(Avatars.GetPhysicsAvatarPath(uidstr, AvatarSize.Large));
                File.Delete(Avatars.GetPhysicsAvatarPath(uidstr, AvatarSize.Medium));
                File.Delete(Avatars.GetPhysicsAvatarPath(uidstr, AvatarSize.Small));
            }
        }

        /// <summary>
        /// 获取中等头像地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns>图片地址</returns>
        public static string GetAvatarUrl(int uid)
        {
            return GetAvatarUrl(uid, AvatarSize.Medium);
        }

        /// <summary>
        /// 设置用户头像
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="filename">原始jpg图片地址,如:/files/avator.jpg</param>
        /// <param name="maxsize">原始图片缩放到的最大参考尺寸,然后等比例缩放；如果为0则不缩放</param>
        /// <param name="coords">size(原始target图片宽高尺寸),x,y,w,h</param>
        /// <returns>返回消息,为空则设置成功</returns>
        public static string UserAvatorSet(int uid, string filename, int maxsize, string coords)
        {
            if (coords == "")
            {
                coords = "350,0,0,150,150";
            }

            if (!FileUtil.FileExists(Utils.GetMapPath(filename)))
            {
                return "图片不存在";
            }

            if (!Utils.IsImgFilename(filename))
            {
                return "请上传图片格式的文件";
            }
            string uidstr = FormatUid(uid);

            string original = GetPhysicsAvatarPath(uidstr, AvatarSize.Original);
            string large = GetPhysicsAvatarPath(uidstr, AvatarSize.Large);
            string medium = GetPhysicsAvatarPath(uidstr, AvatarSize.Medium);
            string small = GetPhysicsAvatarPath(uidstr, AvatarSize.Small);

            string[] coordsarray = coords.Split(',');

            string avatorpath = original.Substring(0, original.LastIndexOf("\\"));
            FileUtil.CreateFolder(avatorpath);

            int targetsize = TypeParse.StrToInt(coordsarray[0]);
            if (targetsize <= 150) targetsize = 350;
            int target_x = TypeParse.StrToInt(coordsarray[1]);
            int target_y = TypeParse.StrToInt(coordsarray[2]);
            int target_w = TypeParse.StrToInt(coordsarray[3]);
            if (target_w <= 0) targetsize = 150;

            if (original != Utils.GetMapPath(filename))
            {
                if (maxsize <= 0)
                {
                    FileUtil.MoveFile(Utils.GetMapPath(filename), original);
                }
                else
                {
                    Thumbnail.MakeThumbnailImage(Bitmap.FromFile(Utils.GetMapPath(filename)), original, targetsize, targetsize, false);
                }
            }
            Thumbnail.MakeSquareImage(GetTargetThumbImg(filename, targetsize), large, 150, new Rectangle(target_x, target_y, target_w, target_w));
            Thumbnail.MakeSquareImage(GetTargetThumbImg(filename, targetsize), medium, 80, new Rectangle(target_x, target_y, target_w, target_w));
            Thumbnail.MakeSquareImage(GetTargetThumbImg(filename, targetsize), small, 50, new Rectangle(target_x, target_y, target_w, target_w));
            return "";
        }

        static Image GetTargetThumbImg(string filename, int targetsize)
        {
            return Thumbnail.GetThumbnailImage(Bitmap.FromFile(Utils.GetMapPath(filename)), targetsize, targetsize, true);
        }
    }
}
