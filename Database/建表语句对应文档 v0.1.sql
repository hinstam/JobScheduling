
--һ���������-- SQL SERVER

--1
/*==============================================================*/
/* Table: "TB_USER-�û�������"                                       */
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
/* Table: "TB_DEPT-������"                                         */
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
/* Table: "TB_USER_ROLE-��ɫȨ�ޱ�"                                  */
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
/* Table: "TB_LOG-������־��"                                        */
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



--���������������-- SQL SERVER
--������ֻ��2�����ݣ����๫˾������A��
--���롰���๫˾��
INSERT INTO TB_DEPT
(
   DEPT_CODE,
   DEPT_NAME,
   LEV,
   CREATE_TIME,
   invalid_flag
)
VALUES('100000000','���๫˾',1,getdate(),'0');

--���롰����A��
INSERT INTO TB_DEPT
(
   DEPT_CODE,
   DEPT_NAME,
   UPPER_DEPT_ID,
   LEV,
   CREATE_TIME,
   invalid_flag
)
VALUES('100000001','����A',1,2,getdate(),'0');

--�û�Ȩ�ޱ�ֻ��2�����ݣ�ϵͳ����Ա�����乤����Ա��
--���롰ϵͳ����Ա��
INSERT INTO TB_USER_ROLE
(
   ROLE_NAME,
   ROLE_DESC,
   CREATE_TIME,
   invalid_flag
)
VALUES('ϵͳ����Ա','����Ȩ�ޣ��Ű����ģ���ϵͳ����ģ��',getdate(),'0');

--���롰���乤����Ա��
INSERT INTO TB_USER_ROLE
(
   ROLE_NAME,
   ROLE_DESC,
   CREATE_TIME,
   invalid_flag
)
VALUES('���乤����Ա','ֻ���Ű����ģ��',getdate(),'0');
