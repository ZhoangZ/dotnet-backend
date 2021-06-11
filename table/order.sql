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

 Date: 11/06/2021 11:07:32
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for order
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` int(10) NOT NULL,
  `order_status` int(10) NULL DEFAULT NULL,
  `created_date` date NULL DEFAULT NULL,
  `address_delivery` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `name_consumer` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `phone_number` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `total_price` decimal(10, 0) UNSIGNED NOT NULL DEFAULT 0,
  `total_item` int(11) UNSIGNED NOT NULL DEFAULT 0,
  `deleled` int(1) UNSIGNED NOT NULL DEFAULT 0,
  `payment_id` bigint(20) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `iduser`(`user_id`) USING BTREE,
  INDEX `donhang2`(`order_status`) USING BTREE,
  CONSTRAINT `order_ibfk_2` FOREIGN KEY (`order_status`) REFERENCES `order_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE = InnoDB AUTO_INCREMENT = 34 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of order
-- ----------------------------
INSERT INTO `order` VALUES (1, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (2, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (3, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (4, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (5, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (6, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (7, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (8, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (9, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (10, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (11, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (12, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (13, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (14, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (15, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (16, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (17, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (18, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (19, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (20, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (21, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 0);
INSERT INTO `order` VALUES (22, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (23, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (24, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (25, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (26, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (27, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (28, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0, NULL);
INSERT INTO `order` VALUES (29, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (30, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (31, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, NULL);
INSERT INTO `order` VALUES (32, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 20);
INSERT INTO `order` VALUES (33, 25, NULL, NULL, 'An Bình', NULL, NULL, 83403000, 3, 0, 21);

SET FOREIGN_KEY_CHECKS = 1;
