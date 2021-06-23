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

 Date: 23/06/2021 22:13:57
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for ram
-- ----------------------------
DROP TABLE IF EXISTS `ram`;
CREATE TABLE `ram`  (
  `id` int(2) NOT NULL AUTO_INCREMENT,
  `ram` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `actived` bit(1) NOT NULL DEFAULT b'1',
  `deleted` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ram
-- ----------------------------
INSERT INTO `ram` VALUES (1, '2 GB', b'1', b'0');
INSERT INTO `ram` VALUES (2, '3 GB', b'1', b'0');
INSERT INTO `ram` VALUES (3, '4 GB', b'1', b'0');
INSERT INTO `ram` VALUES (4, '6 GB', b'1', b'0');
INSERT INTO `ram` VALUES (5, '8 GB', b'1', b'0');
INSERT INTO `ram` VALUES (6, '12 GB', b'1', b'0');
INSERT INTO `ram` VALUES (10, '100 GB', b'1', b'0');
INSERT INTO `ram` VALUES (11, '2200 GB', b'1', b'1');
INSERT INTO `ram` VALUES (12, NULL, b'1', b'0');

SET FOREIGN_KEY_CHECKS = 1;
