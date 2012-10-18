/*
 * SimpleModal Basic Modal Dialog
 * http://www.ericmmartin.com/projects/simplemodal/
 * http://code.google.com/p/simplemodal/
 *
 * Copyright (c) 2010 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Revision: $Id: basic.js 254 2010-07-23 05:14:44Z emartin24 $
 */

jQuery(function ($) {
	//Load dialog on page load
	//$('#basic-modal-content').modal();

	//Sample AJAX Load
    $('.basic').click(function(e) { $('#basic-modal-content').modal(); $('<div></div>').load('../../forgot_password.aspx').modal(); return false; });

    //User Maintenance Tools Modal window script
    $('.user_modal').click(function(e) { $('<div></div>').load('../../menus/modal/user_modal.aspx').modal(); return false; });

    //Assessment Maintenance Tools Modal window script
    $('.assessment_modal').click(function(e) { $('<div></div>').load('../../menus/modal/assessment_modal.aspx').modal(); return false; });

    $('.view_image_modal').click(function(e) { $('<div></div>').load('../../menus/modal/view_image_modal.aspx').modal(); return false; });
    $('.parent_act_modal').click(function(e) { $('<div></div>').load('../../menus/modal/parent_act_modal.aspx').modal(); return false; });
    $('.settings_modal').click(function(e) { $('<div></div>').load('../../menus/modal/settings_modal.aspx').modal(); return false; });  	    
	});
	
	