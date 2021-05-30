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

 Date: 30/05/2021 21:21:12
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
  `actived` bit(1) NULL DEFAULT b'0',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 18 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cart_item
-- ----------------------------
INSERT INTO `cart_item` VALUES (17, 3, 1, '2021-05-30 13:29:47', '2021-05-30 13:29:47', b'0', 10, b'1');

-- ----------------------------
-- Triggers structure for table cart_item
-- ----------------------------
DROP TRIGGER IF EXISTS `before_insert_cart_item`;
delimiter ;;
CREATE TRIGGER `before_insert_cart_item` BEFORE INSERT ON `cart_item` FOR EACH ROW BEGIN
			SELECT price into @price from product_2 where id = new.product_id;
			if new.actived = 1 then
					UPDATE cart set cart.total_price=cart.total_price+ @price * new.amount, cart.total_item=cart.total_item+1 WHERE cart.id=new.cart_id;
				end if;
			
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
				if (old.actived = 0 and new.actived = 1) then
					UPDATE cart set cart.total_price=(cart.total_price+ @price * new.amount) , cart.total_item = cart.total_item+1 WHERE cart.id=new.cart_id;
				end if;
				if (old.actived = 1 and new.actived = 0) then
					UPDATE cart set cart.total_price=cart.total_price- @price * new.amount, cart.total_item=cart.total_item-1 WHERE cart.id=new.cart_id;
				end if;
				if (old.actived = 1 and new.actived = 1) then
					UPDATE cart set cart.total_price=cart.total_price+ @price * (new.amount-old.amount) WHERE cart.id=new.cart_id;
				end if;

	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table cart_item
-- ----------------------------
DROP TRIGGER IF EXISTS `before_delete_cart_item`;
delimiter ;;
CREATE TRIGGER `before_delete_cart_item` BEFORE DELETE ON `cart_item` FOR EACH ROW BEGIN
				SELECT price into @price from product_2 where id = old.product_id;
			
				if old.actived = 1  then
					UPDATE cart set cart.total_price=cart.total_price- @price * old.amount, cart.total_item=cart.total_item-1 WHERE cart.id=old.cart_id;
				end if;
				

	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
