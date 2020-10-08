(function($){Drupal.Views={};Drupal.behaviors.viewsTabs={attach:function(context){if($.viewsUi&&$.viewsUi.tabs){$('#views-tabset').once('views-processed').viewsTabs({selectedClass:'active'});}$('a.views-remove-link').once('views-processed').click(function(event){var id=$(this).attr('id').replace('views-remove-link-','');$('#views-row-'+id).hide();$('#views-removed-'+id).attr('checked',true);event.preventDefault();});$('a.display-remove-link').addClass('display-processed').click(function(){var id=$(this).attr('id').replace('display-remove-link-','');$('#display-row-'+id).hide();$('#display-removed-'+id).attr('checked',true);return false;});}};Drupal.Views.parseQueryString=function(query){var args={};var pos=query.indexOf('?');if(pos!=-1){query=query.substring(pos+1);}var pairs=query.split('&');for(var i in pairs){if(typeof(pairs[i])=='string'){var pair=pairs[i].split('=');if(pair[0]!='q'&&pair[1]){args[decodeURIComponent(pair[0].replace(/\+/g,' '))]=decodeURIComponent(pair[1].replace(/\+/g,' '));}}}return args;};Drupal.Views.parseViewArgs=function(href,viewPath){if(Drupal.settings.pathPrefix){var viewPath=Drupal.settings.pathPrefix+viewPath;}var returnObj={};var path=Drupal.Views.getPath(href);if(viewPath&&path.substring(0,viewPath.length+1)==viewPath+'/'){var args=decodeURIComponent(path.substring(viewPath.length+1,path.length));returnObj.view_args=args;returnObj.view_path=path;}return returnObj;};Drupal.Views.pathPortion=function(href){var protocol=window.location.protocol;if(href.substring(0,protocol.length)==protocol){href=href.substring(href.indexOf('/',protocol.length+2));}return href;};Drupal.Views.getPath=function(href){href=Drupal.Views.pathPortion(href);href=href.substring(Drupal.settings.basePath.length,href.length);if(href.substring(0,3)=='?q='){href=href.substring(3,href.length);}var chars=['#','?','&'];for(var i in chars){if(href.indexOf(chars[i])>-1){href=href.substr(0,href.indexOf(chars[i]));}}return href;};})(jQuery);