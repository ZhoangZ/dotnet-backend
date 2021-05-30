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

 Date: 30/05/2021 18:18:20
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for cart_item
-- ----------------------------
DROP TABLE IF EXISTS `cart_item`;
CREATE TABLE `cart_item`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `product_id` int(255) NULL DEFAULT NULL,
  `cart_id` bigint(20) NOT NULL,
  `create_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `update_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `deleted` bit(1) NOT NULL DEFAULT b'0',
  `amount` int(255) NOT NULL DEFAULT 0,
  `total_price` decimal(10, 0) UNSIGNED NULL DEFAULT 0,
  `actived` bit(1) NULL DEFAULT b'0',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 16 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cart_item
-- ----------------------------
INSERT INTO `cart_item` VALUES (1, 1, 1, '2021-05-30 09:27:17', '2021-05-30 09:27:17', b'0', 0, 100, b'0');
INSERT INTO `cart_item` VALUES (14, 4, 1, '2021-05-30 11:11:19', '2021-05-30 11:11:19', b'0', 4, 26200000, b'0');
INSERT INTO `cart_item` VALUES (15, 3, 1, '2021-05-30 11:16:37', '2021-05-30 11:16:37', b'0', 1, 2520000, b'0');

-- ----------------------------
-- Triggers structure for table cart_item
-- ----------------------------
DROP TRIGGER IF EXISTS `before_insert_cart_item`;
delimiter ;;
CREATE TRIGGER `before_insert_cart_item` BEFORE INSERT ON `cart_item` FOR EACH ROW BEGIN
			SELECT price into @price from product_2 where id = new.product_id;
			SET  new.total_price=new.amount * @price;
			
	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table cart_item
-- ----------------------------
DROP TRIGGER IF EXISTS `before_update_cart_item`;
delimiter ;;
CREATE TRIGGER `before_update_cart_item` BEFORE UPDATE ON `cart_item` FOR EACH ROW BEGIN
				SELECT price into @price from product_2 where id = new.product_id;
				SET  new.total_price=new.amount * @price;

	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
