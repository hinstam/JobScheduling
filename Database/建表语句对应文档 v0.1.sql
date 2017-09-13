
--一、建表语句-- SQL SERVER

--1
/*==============================================================*/
/* Table: "TB_USER-用户基础表"                                       */
/*==============================================================*/
create table TB_USER
(
   user_id              int  identity(1,1)             not null PRIMARY KEY,
   login_name           varchar(30)                    null,
   password             varchar(40)                    null,
   user_name            varchar(30)                    null,
   Work_no              varchar(30)                    null,
   Dept_id              int                            null,
   Role_id              int                            null,
   create_time          datetime                       null,
   invalid_flag         varchar(2)                     null,
   Remark               varchar(4000)                  null
);

--2
/*==============================================================*/
/* Table: "TB_DEPT-机构表"                                         */
/*==============================================================*/
create table TB_DEPT
(
   DEPT_ID              int identity(1,1)             not null PRIMARY KEY,
   DEPT_CODE            varchar(18)                    null,
   DEPT_NAME            varchar(40)                    null,
   UPPER_DEPT_ID        int                            null,
   REMARK               varchar(4000)                  null,
   LEV                  int                            null,
   CREATE_TIME          datetime                       null,
   invalid_flag         varchar(2)                     null
);

--3
/*==============================================================*/
/* Table: "TB_USER_ROLE-角色权限表"                                  */
/*==============================================================*/
create table TB_USER_ROLE
(
   ROLE_ID              int identity(1,1)             not null PRIMARY KEY,
   ROLE_NAME            varchar(40)                    null,
   ROLE_DESC            varchar(400)                   null,
   REMARK               varchar(4000)                  null,
   CREATE_TIME          datetime                       null,
   invalid_flag         varchar(2)                     null
);

--4
/*==============================================================*/
/* Table: "TB_LOG-操作日志表"                                        */
/*==============================================================*/
create table TB_LOG 
(
   LOG_ID               int identity(1,1)             not null PRIMARY KEY,
   MODULE_NAME          varchar(40)                    null,
   OP_TYPE              varchar(20)                    null,
   OP_CONTENT           varchar(2000)                  null,
   CREATE_TIME          Datetime                       null,
   CREATE_USER_ID       int                            null
);



--二、插入测试数据-- SQL SERVER
--机构表只有2条数据（宝洁公司，车间A）
--插入“宝洁公司”
INSERT INTO TB_DEPT
(
   DEPT_CODE,
   DEPT_NAME,
   LEV,
   CREATE_TIME,
   invalid_flag
)
VALUES('100000000','宝洁公司',1,getdate(),'0');

--插入“车间A”
INSERT INTO TB_DEPT
(
   DEPT_CODE,
   DEPT_NAME,
   UPPER_DEPT_ID,
   LEV,
   CREATE_TIME,
   invalid_flag
)
VALUES('100000001','车间A',1,2,getdate(),'0');

--用户权限表只有2条数据（系统管理员，车间工作人员）
--插入“系统管理员”
INSERT INTO TB_USER_ROLE
(
   ROLE_NAME,
   ROLE_DESC,
   CREATE_TIME,
   invalid_flag
)
VALUES('系统管理员','所有权限，排班管理模块和系统管理模块',getdate(),'0');

--插入“车间工作人员”
INSERT INTO TB_USER_ROLE
(
   ROLE_NAME,
   ROLE_DESC,
   CREATE_TIME,
   invalid_flag
)
VALUES('车间工作人员','只有排班管理模块',getdate(),'0');
