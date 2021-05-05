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

 Date: 06/05/2021 00:24:33
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `type` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `active` varchar(1) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT '1',
  `updated_by` int(11) NULL DEFAULT utc_timestamp,
  `created_by` int(11) NULL DEFAULT utc_timestamp,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES (1, 'USER', 'Authentication User', '2', '1', NULL, NULL);
INSERT INTO `role` VALUES (2, 'USER', 'Authentication User', '2', '1', NULL, NULL);
INSERT INTO `role` VALUES (3, 'USER', 'Authentication User', '2', '1', NULL, NULL);
INSERT INTO `role` VALUES (4, 'USER', 'Authentication User', '2', '1', NULL, NULL);
INSERT INTO `role` VALUES (5, 'USER', 'Authentication User', '2', '1', NULL, NULL);
INSERT INTO `role` VALUES (6, 'USER', 'Authentication User', '2', '1', NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
