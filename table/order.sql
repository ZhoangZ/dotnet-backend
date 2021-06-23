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

 Date: 23/06/2021 23:23:53
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
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `note` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `iduser`(`user_id`) USING BTREE,
  INDEX `donhang2`(`order_status`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 52 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of order
-- ----------------------------
INSERT INTO `order` VALUES (42, 25, 1, '2021-06-22', 'An Bình', NULL, NULL, 55602000, 2, 0, 26, NULL, NULL);
INSERT INTO `order` VALUES (43, 25, 1, '2021-06-14', 'An Bình', NULL, NULL, 55602000, 2, 0, 27, NULL, NULL);
INSERT INTO `order` VALUES (44, 25, 1, '2021-06-23', 'An Bình', NULL, NULL, 55602000, 2, 0, 28, NULL, NULL);
INSERT INTO `order` VALUES (45, 25, 1, '2021-06-23', 'An Bình', NULL, NULL, 55602000, 2, 0, 29, NULL, NULL);
INSERT INTO `order` VALUES (46, 25, 1, '2021-06-23', 'An Bình', 'Lê Tấn Hoàng', '0399155950', 55602000, 2, 0, 30, 'tanhoang99.999@gmail.com', '...');
INSERT INTO `order` VALUES (47, 25, 1, '2021-06-23', 'An Bình', 'Lê Tấn Hoàng', '0399155950', 55602000, 2, 0, 31, 'tanhoang99.999@gmail.com', '...');
INSERT INTO `order` VALUES (48, 25, 1, '2021-06-23', 'An Bình', 'Lê Tấn Hoàng', '0399155950', 55602000, 2, 0, 32, 'tanhoang99.999@gmail.com', '...');
INSERT INTO `order` VALUES (49, 25, 1, '2021-06-23', 'An Bình', 'Lê Tấn Hoàng', '0399155950', 55602000, 2, 0, 33, 'tanhoang99.999@gmail.com', '...');
INSERT INTO `order` VALUES (50, 21, 4, '2021-06-23', 'An Bình', 'Lê Tấn Hoàng', '0399155950', 55602000, 2, 0, 34, 'tanhoang99.999@gmail.com', '...');
INSERT INTO `order` VALUES (51, 21, 1, '2021-06-23', 'An Bình', 'Lê Tấn Hoàng', '0399155950', 55602000, 2, 0, 35, 'tanhoang99.999@gmail.com', '...');

SET FOREIGN_KEY_CHECKS = 1;
