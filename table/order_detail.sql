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

 Date: 11/06/2021 11:07:38
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
  `saled_price` bigint(255) NOT NULL,
  `price` int(255) NOT NULL,
  `product_specific_id` bigint(20) NOT NULL,
  `create_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `update_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `deleted` bit(1) NOT NULL DEFAULT b'0',
  `amount` int(255) NOT NULL DEFAULT 0,
  `actived` bit(1) NOT NULL DEFAULT b'0',
  `sale_rate` int(255) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `chitietdonhang1`(`order_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 43 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of order_detail
-- ----------------------------
INSERT INTO `order_detail` VALUES (1, 12, 0, 30890000, 1, '2021-06-11 03:14:09', '2021-06-11 03:14:09', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (2, 12, 0, 30890000, 2, '2021-06-11 03:14:09', '2021-06-11 03:14:09', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (3, 13, 0, 30890000, 1, '2021-06-11 03:15:35', '2021-06-11 03:15:35', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (4, 13, 0, 30890000, 2, '2021-06-11 03:15:35', '2021-06-11 03:15:35', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (5, 14, 0, 30890000, 1, '2021-06-11 03:24:46', '2021-06-11 03:24:46', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (6, 14, 0, 30890000, 2, '2021-06-11 03:24:46', '2021-06-11 03:24:46', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (7, 15, 0, 30890000, 1, '2021-06-11 03:25:57', '2021-06-11 03:25:57', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (8, 15, 0, 30890000, 2, '2021-06-11 03:25:57', '2021-06-11 03:25:57', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (9, 16, 0, 30890000, 1, '2021-06-11 03:27:34', '2021-06-11 03:27:34', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (10, 16, 0, 30890000, 2, '2021-06-11 03:27:34', '2021-06-11 03:27:34', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (11, 17, 0, 30890000, 1, '2021-06-11 03:30:56', '2021-06-11 03:30:56', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (12, 17, 0, 30890000, 2, '2021-06-11 03:30:56', '2021-06-11 03:30:56', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (13, 18, 0, 30890000, 1, '2021-06-11 03:32:22', '2021-06-11 03:32:22', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (14, 18, 0, 30890000, 2, '2021-06-11 03:32:22', '2021-06-11 03:32:22', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (15, 19, 0, 30890000, 1, '2021-06-11 03:33:18', '2021-06-11 03:33:18', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (16, 19, 0, 30890000, 2, '2021-06-11 03:33:18', '2021-06-11 03:33:18', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (17, 20, 0, 30890000, 1, '2021-06-11 03:34:33', '2021-06-11 03:34:33', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (18, 20, 0, 30890000, 2, '2021-06-11 03:34:33', '2021-06-11 03:34:33', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (19, 21, 0, 30890000, 1, '2021-06-11 03:36:16', '2021-06-11 03:36:16', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (20, 21, 0, 30890000, 2, '2021-06-11 03:36:16', '2021-06-11 03:36:16', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (21, 22, 0, 30890000, 1, '2021-06-11 03:37:54', '2021-06-11 03:37:54', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (22, 22, 0, 30890000, 2, '2021-06-11 03:37:55', '2021-06-11 03:37:55', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (23, 23, 0, 30890000, 1, '2021-06-11 03:39:12', '2021-06-11 03:39:12', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (24, 23, 0, 30890000, 2, '2021-06-11 03:39:12', '2021-06-11 03:39:12', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (25, 24, 0, 30890000, 1, '2021-06-11 03:40:26', '2021-06-11 03:40:26', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (26, 24, 0, 30890000, 2, '2021-06-11 03:40:26', '2021-06-11 03:40:26', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (27, 25, 0, 30890000, 1, '2021-06-11 03:42:21', '2021-06-11 03:42:21', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (28, 25, 0, 30890000, 2, '2021-06-11 03:42:21', '2021-06-11 03:42:21', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (29, 26, 0, 30890000, 1, '2021-06-11 03:43:05', '2021-06-11 03:43:05', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (30, 26, 0, 30890000, 2, '2021-06-11 03:43:05', '2021-06-11 03:43:05', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (31, 27, 0, 30890000, 1, '2021-06-11 03:43:58', '2021-06-11 03:43:58', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (32, 27, 0, 30890000, 2, '2021-06-11 03:43:58', '2021-06-11 03:43:58', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (33, 29, 0, 30890000, 1, '2021-06-11 03:46:04', '2021-06-11 03:46:04', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (34, 29, 0, 30890000, 2, '2021-06-11 03:46:04', '2021-06-11 03:46:04', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (35, 30, 0, 30890000, 1, '2021-06-11 03:47:53', '2021-06-11 03:47:53', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (36, 30, 0, 30890000, 2, '2021-06-11 03:47:53', '2021-06-11 03:47:53', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (37, 31, 0, 30890000, 1, '2021-06-11 03:48:35', '2021-06-11 03:48:35', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (38, 31, 0, 30890000, 2, '2021-06-11 03:48:35', '2021-06-11 03:48:35', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (39, 32, 0, 30890000, 1, '2021-06-11 03:49:38', '2021-06-11 03:49:38', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (40, 32, 0, 30890000, 2, '2021-06-11 03:49:38', '2021-06-11 03:49:38', b'0', 1, b'1', 0);
INSERT INTO `order_detail` VALUES (41, 33, 0, 30890000, 1, '2021-06-11 03:56:58', '2021-06-11 03:56:58', b'0', 2, b'1', 0);
INSERT INTO `order_detail` VALUES (42, 33, 0, 30890000, 2, '2021-06-11 03:56:58', '2021-06-11 03:56:58', b'0', 1, b'1', 0);

-- ----------------------------
-- Triggers structure for table order_detail
-- ----------------------------
DROP TRIGGER IF EXISTS `before_insert_order_detail`;
delimiter ;;
CREATE TRIGGER `before_insert_order_detail` BEFORE INSERT ON `order_detail` FOR EACH ROW BEGIN
				SELECT SALE_PRICE into @SALE_PRICE from product_specific where id = new.product_specific_id;
				SELECT sale_rate into @sale_rate from product_specific where id = new.product_specific_id;
				SELECT price into @price from product_specific where id = new.product_specific_id;
				set new.SALE_RATE = @sale_rate;
				set new.PRICE = @price;
				set new.saled_price = (new.sale_rate*new.price)/100;
				UPDATE `order` set `order`.total_price=`order`.total_price+ @SALE_PRICE * new.amount, `order`.total_item=`order`.total_item+new.amount WHERE `order`.id=new.order_id;
			
			
	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table order_detail
-- ----------------------------
DROP TRIGGER IF EXISTS `before_delete_order_detail`;
delimiter ;;
CREATE TRIGGER `before_delete_order_detail` BEFORE DELETE ON `order_detail` FOR EACH ROW BEGIN
				
					UPDATE `order` set `order`.total_price=`order`.total_price- old.saled_price * old.amount, `order`.total_item=`order`.total_item-old.amount WHERE `order`.id=old.order_id;
				
				

	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
