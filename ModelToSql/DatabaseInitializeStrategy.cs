using ModelToSql;
using ModelToSql.Enums;
using ModelToSql.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ModelToSql.Infrastructure
{
    /// <summary>
    /// 数据库初始化策略
    ///1.MigrateDatabaseToLatestVersion：使用Code First数据库迁移策略，将数据库更新到最新版本
    ///2.NullDatabaseInitializer：一个什么都不干的数据库初始化器
    ///3.CreateDatabaseIfNotExists：顾名思义，如果数据库不存在则新建数据库
    ///4.DropCreateDatabaseAlways：无论数据库是否存在，始终重建数据库
    ///5.DropCreateDatabaseIfModelChanges：仅当领域模型发生变化时才重建数据库
    /// </summary>
    public class DatabaseInitializeStrategy
        : DbMigrationsConfiguration<EasyDbContext>
    {
        public DatabaseInitializeStrategy()
        {
            AutomaticMigrationsEnabled = true;  //获取或设置 指示迁移数据库时是否可使用自动迁移的值。
            //获取或设置 指示是否可接受自动迁移期间的数据丢失的值。如果设置为false，则将在数据丢失可能作为自动迁移一部分出现时引发异常。
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(EasyDbContext context)
        {
            var Tenant = context.Users.FirstOrDefault(p => p.SysRealName == "陈亢龙");
            Tenant = Tenant ?? new Sys_User();
            Tenant.SysRealName = "陈亢龙";
            Tenant.Password = "666666";
            Tenant.AddDate = DateTime.Now;
            Tenant.AddUserId = Guid.NewGuid().ToString("D");
            context.Users.AddOrUpdate(p => p.SysRealName, Tenant);

            var syscompany = context.Companys.FirstOrDefault(p => p.companyName == "龙骋");//公司名称
            syscompany = syscompany ?? new Sys_Company();
            syscompany.companyName = "龙骋";
            syscompany.sort = 1;
            syscompany.AddDate = DateTime.Now;
            syscompany.AddUserId = Tenant.AddUserId;
            context.Companys.AddOrUpdate(p => p.companyName, syscompany);

            var depart = context.SysDeparts.FirstOrDefault(p => p.DepartName == "管理维护部门");
            depart = depart ?? new Sys_Depart();
            depart.DepartName = "管理维护部门";
            depart.Sys_CompanyId = syscompany.Id;
            depart.AddDate = DateTime.Now;
            depart.AddUserId = Tenant.AddUserId;
            context.SysDeparts.AddOrUpdate(p => p.DepartName, depart);

            var group = context.sysGroups.FirstOrDefault(p => p.GroupName == "维护一组");
            group = group ?? new Sys_Group();
            group.GroupName = "维护一组";
            group.SysDepartId = depart.Id;
            group.AddDate = DateTime.Now;
            group.AddUserId = Tenant.AddUserId;
            context.sysGroups.AddOrUpdate(p => p.GroupName, group);

            ButtonDic btn = context.ButtonDics.FirstOrDefault(p => p.IsDeleted == false && p.BtnType == ButtonEnum.Grid);
            ButtonDic EditGrid = null;
            ButtonDic DeleteGrid = null;
            if (btn == null)
            {
                context.ButtonDics.AddOrUpdate(
                      p => new { p.BtnName, p.BtnType },
                      new ButtonDic { BtnName = "浏览", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-file-o", BtnType = ButtonEnum.Grid, Sort = 1 },
                      new ButtonDic { BtnName = "新增", BtnClass = "btn btn-xs purple", BtnIco = "fa fa-plus", BtnType = ButtonEnum.Grid, Sort = 2 },
                     EditGrid = new ButtonDic { BtnName = "编辑", BtnClass = "btn btn-xs green", BtnIco = "fa fa-edit", BtnType = ButtonEnum.Grid, Sort = 3 },
                     DeleteGrid = new ButtonDic { BtnName = "删除", BtnClass = "btn btn-xs red", BtnIco = "fa  fa-trash-o", BtnType = ButtonEnum.Grid, Sort = 4 },
                      new ButtonDic { BtnName = "审核", BtnClass = "btn btn-xs blue", BtnIco = "fa  fa-search", BtnType = ButtonEnum.Grid, Sort = 5 },
                      new ButtonDic { BtnName = "生成", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-file-text", BtnType = ButtonEnum.Grid, Sort = 6 },
                      new ButtonDic { BtnName = "上传", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-cloud-upload", BtnType = ButtonEnum.Grid, Sort = 7 },
                      new ButtonDic { BtnName = "下载", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-cloud-download ", BtnType = ButtonEnum.Grid, Sort = 8 },
                      new ButtonDic { BtnName = "开始", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-check", BtnType = ButtonEnum.Grid, Sort = 9 },
                      new ButtonDic { BtnName = "终止", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-warning", BtnType = ButtonEnum.Grid, Sort = 10 },
                      new ButtonDic { BtnName = "设置权限", BtnClass = "btn btn-xs blue", BtnIco = "fa  fa-lock", BtnType = ButtonEnum.Grid, Sort = 11 }
                    );

            }
            if (EditGrid != null)
            {
                context.UseButtonDics.AddOrUpdate(
                   p => new { p.BtnName, p.BtnType, p.ResourceId },
                   new UseButtonDic { BtnName = EditGrid.BtnName, ButtonDicId = EditGrid.Id, BtnUri = "", ResourceId = Guid.NewGuid(), BtnClass = EditGrid.BtnClass, BtnIco = EditGrid.BtnIco, BtnType = ButtonEnum.Grid, Sort = 1 }
                );
            }
            if (DeleteGrid != null)
            {
                context.UseButtonDics.AddOrUpdate(
                   p => new { p.BtnName, p.BtnType, p.ResourceId },
                   new UseButtonDic { BtnName = DeleteGrid.BtnName, ButtonDicId = DeleteGrid.Id, BtnUri = "", ResourceId = Guid.NewGuid(), BtnClass = DeleteGrid.BtnClass, BtnIco = DeleteGrid.BtnIco, BtnType = ButtonEnum.Grid, Sort = 3 }
                );
            }

            ButtonDic btnToolbar = context.ButtonDics.FirstOrDefault(p => p.IsDeleted == false && p.BtnType == ButtonEnum.ToolBar);
            ButtonDic ToolBarAdd = null;
            if (btnToolbar == null)
            {
                context.ButtonDics.AddOrUpdate(
                      p => new { p.BtnName, p.BtnType },
                      new ButtonDic { BtnName = "浏览", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-file-o", BtnType = ButtonEnum.ToolBar, Sort = 1 },
                      ToolBarAdd = new ButtonDic { BtnName = "新增", BtnClass = "btn btn-xs purple", BtnIco = "fa fa-plus", BtnType = ButtonEnum.ToolBar, Sort = 2 },
                      new ButtonDic { BtnName = "编辑", BtnClass = "btn btn-xs green", BtnIco = "fa fa-edit", BtnType = ButtonEnum.ToolBar, Sort = 3 },
                      new ButtonDic { BtnName = "删除", BtnClass = "btn btn-xs red", BtnIco = "fa  fa-trash-o", BtnType = ButtonEnum.ToolBar, Sort = 4 },
                      new ButtonDic { BtnName = "审核", BtnClass = "btn btn-xs blue", BtnIco = "fa  fa-search", BtnType = ButtonEnum.ToolBar, Sort = 5 },
                      new ButtonDic { BtnName = "导入", BtnClass = "btn btn-xs blue", BtnIco = "fa  fa-reply", BtnType = ButtonEnum.ToolBar, Sort = 6 },
                      new ButtonDic { BtnName = "导出", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-share", BtnType = ButtonEnum.ToolBar, Sort = 7 },
                      new ButtonDic { BtnName = "生成", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-file-text", BtnType = ButtonEnum.ToolBar, Sort = 8 },
                      new ButtonDic { BtnName = "打印", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-print", BtnType = ButtonEnum.ToolBar, Sort = 9 },
                      new ButtonDic { BtnName = "上传", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-cloud-upload", BtnType = ButtonEnum.ToolBar, Sort = 10 },
                      new ButtonDic { BtnName = "下载", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-cloud-download ", BtnType = ButtonEnum.ToolBar, Sort = 11 },
                      new ButtonDic { BtnName = "开始", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-check", BtnType = ButtonEnum.ToolBar, Sort = 12 },
                      new ButtonDic { BtnName = "终止", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-warning", BtnType = ButtonEnum.ToolBar, Sort = 13 },
                      new ButtonDic { BtnName = "设置权限", BtnClass = "btn btn-xs blue", BtnIco = "fa fa-warning", BtnType = ButtonEnum.ToolBar, Sort = 14 }
                    );

            }
            if (ToolBarAdd != null)
            {
                context.UseButtonDics.AddOrUpdate(
                   p => new { p.BtnName, p.BtnType, p.ResourceId },
                   new UseButtonDic { BtnName = ToolBarAdd.BtnName, ButtonDicId = ToolBarAdd.Id, BtnUri = "", ResourceId = Guid.NewGuid(), BtnClass = ToolBarAdd.BtnClass, BtnIco = ToolBarAdd.BtnIco, BtnType = ButtonEnum.ToolBar, Sort = 2 }
                );
            }
        }
    }
}