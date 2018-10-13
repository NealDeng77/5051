using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Backend;
using _5051.Models;

namespace _5051.Maintain
{

    public class AvatarItemMaintenance
    {
        /// <summary>
        /// Refersh Avatars, walks the Avatar Data in the DB, with the Avatar Defaults from the Helper, and restores defaults that are not there. 
        /// Call this function after uploading new avatars etc
        /// Or changing an avatar settings
        /// </summary>
        public bool RefreshAvatars()
        {

            return true;
        }
    }
}