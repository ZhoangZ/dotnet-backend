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

 Date: 09/06/2021 08:27:18
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for order_detail
-- ----------------------------
DROP TABLE IF EXISTS `order_detail`;
CREATE TABLE `order_detail`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `order_id` bigint(20) NULL DEFAULT NULL,
  `saled_price` bigint(255) NULL DEFAULT NULL,
  `price` int(255) NULL DEFAULT NULL,
  `active` int(255) NULL DEFAULT 1,
  `product_specific_id` bigint(20) NOT NULL,
  `create_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `update_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `deleted` bit(1) NOT NULL DEFAULT b'0',
  `amount` int(255) NOT NULL DEFAULT 0,
  `actived` bit(1) NOT NULL DEFAULT b'0',
  `sale_rate` int(255) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `chitietdonhang1`(`order_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 59 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of order_detail
-- ----------------------------
INSERT INTO `order_detail` VALUES (55, 43, NULL, NULL, 1, 1, '2021-06-09 01:24:19', '2021-06-09 01:24:19', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (56, 43, NULL, NULL, 1, 2, '2021-06-09 01:24:20', '2021-06-09 01:24:20', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (57, 44, NULL, NULL, 1, 1, '2021-06-09 01:26:11', '2021-06-09 01:26:11', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (58, 44, NULL, NULL, 1, 2, '2021-06-09 01:26:11', '2021-06-09 01:26:11', b'0', 1, b'1', 0);

SET FOREIGN_KEY_CHECKS = 1;
