/*
 Navicat Premium Data Transfer

 Source Server         : demo
 Source Server Type    : MySQL
 Source Server Version : 100418
 Source Host           : localhost:3306
 Source Schema         : dotnet

 Target Server Type    : MySQL
 Target Server Version : 100418
 File Encoding         : 65001

 Date: 30/05/2021 18:33:21
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for comment
-- ----------------------------
DROP TABLE IF EXISTS `comment`;
CREATE TABLE `comment`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `user_id` int(10) NULL DEFAULT NULL,
  `product_id` int(10) NULL DEFAULT NULL,
  `rate` double(1, 0) NULL DEFAULT NULL,
  `content` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL,
  `created_date` date NULL DEFAULT NULL,
  `active` int(255) NULL DEFAULT 1,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_id`(`user_id`) USING BTREE,
  INDEX `product_id`(`product_id`) USING BTREE,
  CONSTRAINT `comment_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `comment_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `product_2` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of comment
-- ----------------------------
INSERT INTO `comment` VALUES (4, 17, 10, NULL, 'San pham nay rat tuyet', NULL, 1);
INSERT INTO `comment` VALUES (5, 17, 11, 0, 'San pham nay rat tuyet', '0001-01-01', 1);
INSERT INTO `comment` VALUES (6, 19, 11, 4, 'San pham nay rat tuyet boi 19', '2021-05-30', 1);
INSERT INTO `comment` VALUES (7, 19, 11, 4, 'San pham nay rat tuyet boi 19', '2021-05-30', 1);
INSERT INTO `comment` VALUES (8, 18, 11, 4, 'San pham nay rat tuyet boi 19', '2021-05-30', 1);
INSERT INTO `comment` VALUES (9, 16, 11, 5, 'San pham nay rat tuyet boi 16', '2021-05-30', 1);
INSERT INTO `comment` VALUES (10, 15, 11, 4, 'San pham nay rat tuyet boi 15', '2021-05-30', 1);
INSERT INTO `comment` VALUES (11, 14, 11, 4, 'San pham nay rat tuyet boi 14', '2021-05-30', 1);
INSERT INTO `comment` VALUES (12, 14, 11, 4, 'San pham nay rat tuyet boi 14', '2021-05-30', 1);

SET FOREIGN_KEY_CHECKS = 1;
