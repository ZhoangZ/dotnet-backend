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

 Date: 13/06/2021 23:41:47
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
  `product_id` int(255) NOT NULL,
  `create_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `update_at` datetime(0) NOT NULL DEFAULT utc_timestamp,
  `deleted` bit(1) NOT NULL DEFAULT b'0',
  `amount` int(255) NOT NULL DEFAULT 0,
  `actived` bit(1) NOT NULL DEFAULT b'0',
  `sale_rate` int(255) UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `chitietdonhang1`(`order_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 52 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of order_detail
-- ----------------------------
INSERT INTO `order_detail` VALUES (51, 42, 3089000, 30890000, 1, '2021-06-13 16:39:17', '2021-06-13 16:39:17', b'0', 2, b'1', 10);

-- ----------------------------
-- Triggers structure for table order_detail
-- ----------------------------
DROP TRIGGER IF EXISTS `before_insert_order_detail`;
delimiter ;;
CREATE TRIGGER `before_insert_order_detail` BEFORE INSERT ON `order_detail` FOR EACH ROW BEGIN
				SELECT SALE_PRICE into @SALE_PRICE from product_2 where id = new.product_id;
				SELECT sale_rate into @sale_rate from product_2 where id = new.product_id;
				SELECT price into @price from product_2 where id = new.product_id;
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
