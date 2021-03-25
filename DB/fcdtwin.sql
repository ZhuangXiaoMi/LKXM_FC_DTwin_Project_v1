/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.175.128(lkxm)
 Source Server Type    : PostgreSQL
 Source Server Version : 130002
 Source Host           : 192.168.175.128:5432
 Source Catalog        : fcdtwin
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 130002
 File Encoding         : 65001

 Date: 25/03/2021 15:45:49
*/


-- ----------------------------
-- Sequence structure for seq_t_test_id
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."seq_t_test_id";
CREATE SEQUENCE "public"."seq_t_test_id" 
INCREMENT 1
MAXVALUE 999999999
CACHE 1
CYCLE ;
COMMENT ON SEQUENCE "public"."seq_t_test_id" IS '测试表主键序列';

-- ----------------------------
-- Table structure for t_test
-- ----------------------------
DROP TABLE IF EXISTS "public"."t_test";
CREATE TABLE "public"."t_test" (
  "id" int8 NOT NULL DEFAULT nextval('seq_t_test_id'::regclass),
  "name" varchar(30) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ''::character varying,
  "age" int4 NOT NULL DEFAULT 0,
  "create_uid" int8 NOT NULL DEFAULT 0,
  "create_at" date NOT NULL,
  "update_uid" int8 NOT NULL DEFAULT 0,
  "update_at" date NOT NULL,
  "normal" int4 NOT NULL DEFAULT 0,
  "version" int4 NOT NULL DEFAULT 0
)
;
COMMENT ON COLUMN "public"."t_test"."id" IS '主键';
COMMENT ON COLUMN "public"."t_test"."name" IS '名称';
COMMENT ON COLUMN "public"."t_test"."age" IS '年龄';
COMMENT ON COLUMN "public"."t_test"."create_uid" IS '创建用户Id';
COMMENT ON COLUMN "public"."t_test"."create_at" IS '创建时间';
COMMENT ON COLUMN "public"."t_test"."update_uid" IS '更新用户Id';
COMMENT ON COLUMN "public"."t_test"."update_at" IS '更新时间';
COMMENT ON COLUMN "public"."t_test"."normal" IS '数据状态：0 正常 1 删除';
COMMENT ON TABLE "public"."t_test" IS '测试表';

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."seq_t_test_id"
OWNED BY "public"."t_test"."id";

-- ----------------------------
-- Primary Key structure for table t_test
-- ----------------------------
ALTER TABLE "public"."t_test" ADD CONSTRAINT "t_test_pkey" PRIMARY KEY ("id");
