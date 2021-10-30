using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class License : DbBase
    {
        [Comment("ログイン状態かどうか確認")]
        public bool UseFlg { get; set; }
        [StringLength(50)]
        public string Hash { get; set; }
        [StringLength(50)]
        public string Salt { get; set; }
    }
}
