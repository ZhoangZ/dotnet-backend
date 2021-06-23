/*
 Navicat Premium Data Transfer

 Source Server         : My SQL
 Source Server Type    : MySQL
 Source Server Version : 100417
 Source Host           : localhost:3306
 Source Schema         : dotnet

 Target Server Type    : MySQL
 Target Server Version : 100417
 File Encoding         : 65001

 Date: 23/06/2021 22:13:49
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for rom
-- ----------------------------
DROP TABLE IF EXISTS `rom`;
CREATE TABLE `rom`  (
  `id` int(2) NOT NULL AUTO_INCREMENT,
  `rom` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `actived` bit(1) NOT NULL DEFAULT b'1',
  `deleted` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of rom
-- ----------------------------
INSERT INTO `rom` VALUES (1, '16 GB', b'1', b'0');
INSERT INTO `rom` VALUES (2, '32 GB', b'1', b'0');
INSERT INTO `rom` VALUES (3, '64 GB', b'1', b'0');
INSERT INTO `rom` VALUES (4, '128 GB', b'1', b'0');
INSERT INTO `rom` VALUES (5, '256 GB', b'1', b'0');
INSERT INTO `rom` VALUES (6, '512 GB', b'1', b'0');
INSERT INTO `rom` VALUES (9, NULL, b'1', b'1');

SET FOREIGN_KEY_CHECKS = 1;
