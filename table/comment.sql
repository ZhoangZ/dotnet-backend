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

 Date: 02/06/2021 08:34:28
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for comment
-- ----------------------------
DROP TABLE IF EXISTS `comment`;
CREATE TABLE `comment`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `user_id` int(10) NOT NULL,
  `product_id` int(255) NOT NULL,
  `rate` double(1, 0) NOT NULL DEFAULT 0,
  `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `created_date` date NOT NULL DEFAULT utc_timestamp,
  `active` int(255) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_id`(`user_id`) USING BTREE,
  INDEX `product_id`(`product_id`) USING BTREE,
  CONSTRAINT `comment_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `product_2` (`ID`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of comment
-- ----------------------------
INSERT INTO `comment` VALUES (4, 17, 11, 0, 'San pham nay rat tuyet', '2021-06-09', 1);
INSERT INTO `comment` VALUES (5, 17, 11, 0, 'San pham nay rat tuyet', '0001-01-01', 1);
INSERT INTO `comment` VALUES (6, 19, 11, 4, 'San pham nay rat tuyet boi 19', '2021-05-30', 1);
INSERT INTO `comment` VALUES (7, 19, 11, 4, 'San pham nay rat tuyet boi 19', '2021-05-30', 1);
INSERT INTO `comment` VALUES (8, 18, 11, 4, 'San pham nay rat tuyet boi 19', '2021-05-30', 1);
INSERT INTO `comment` VALUES (9, 16, 11, 5, 'San pham nay rat tuyet boi 16', '2021-05-30', 1);
INSERT INTO `comment` VALUES (10, 15, 11, 4, 'San pham nay rat tuyet boi 15', '2021-05-30', 1);
INSERT INTO `comment` VALUES (11, 14, 11, 4, 'San pham nay rat tuyet boi 14', '2021-05-30', 1);
INSERT INTO `comment` VALUES (12, 14, 11, 4, 'San pham nay rat tuyet boi 14', '2021-05-30', 1);

SET FOREIGN_KEY_CHECKS = 1;
