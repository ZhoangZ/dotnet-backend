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

 Date: 09/06/2021 08:27:11
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
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `iduser`(`user_id`) USING BTREE,
  INDEX `donhang2`(`order_status`) USING BTREE,
  CONSTRAINT `order_ibfk_2` FOREIGN KEY (`order_status`) REFERENCES `order_status` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE = InnoDB AUTO_INCREMENT = 45 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of order
-- ----------------------------
INSERT INTO `order` VALUES (41, 25, NULL, NULL, 'An Binh', NULL, NULL, 0, 0, 0);
INSERT INTO `order` VALUES (42, 0, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0);
INSERT INTO `order` VALUES (43, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0);
INSERT INTO `order` VALUES (44, 25, NULL, NULL, 'An Bình', NULL, NULL, 0, 0, 0);

SET FOREIGN_KEY_CHECKS = 1;
