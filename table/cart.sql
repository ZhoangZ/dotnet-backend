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

 Date: 02/06/2021 08:10:08
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cart
-- ----------------------------
DROP TABLE IF EXISTS `cart`;
CREATE TABLE `cart`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `total_price` decimal(10, 0) UNSIGNED NOT NULL DEFAULT 0,
  `total_item` int(11) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `iduser`(`user_id`) USING BTREE,
  CONSTRAINT `cart_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cart
-- ----------------------------
INSERT INTO `cart` VALUES (4, 1, 0, 0);
INSERT INTO `cart` VALUES (5, 5, 0, 0);
INSERT INTO `cart` VALUES (6, 6, 0, 0);
INSERT INTO `cart` VALUES (7, 7, 0, 0);
INSERT INTO `cart` VALUES (8, 11, 0, 0);
INSERT INTO `cart` VALUES (9, 12, 0, 0);
INSERT INTO `cart` VALUES (10, 13, 0, 0);
INSERT INTO `cart` VALUES (11, 14, 0, 0);
INSERT INTO `cart` VALUES (12, 15, 0, 0);
INSERT INTO `cart` VALUES (13, 16, 0, 0);
INSERT INTO `cart` VALUES (14, 17, 0, 0);
INSERT INTO `cart` VALUES (15, 18, 0, 0);
INSERT INTO `cart` VALUES (16, 19, 0, 0);
INSERT INTO `cart` VALUES (17, 20, 0, 0);
INSERT INTO `cart` VALUES (18, 21, 0, 0);
INSERT INTO `cart` VALUES (19, 25, 0, 0);

-- ----------------------------
-- Triggers structure for table cart
-- ----------------------------
DROP TRIGGER IF EXISTS `before_delete_cart`;
delimiter ;;
CREATE TRIGGER `before_delete_cart` BEFORE DELETE ON `cart` FOR EACH ROW BEGIN
				DELETE FROM cart_item WHERE cart_id=old.id;
				

	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
