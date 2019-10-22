//upload imgcollect
function fileImgDialogComplete(numFilesSelected, numFilesQueued) {
    this.startUpload();
};

function uploadImgSuccess(file, serverData) {
    try {
        var attach = {};
        attach.id = serverData.split("*sta*")[0];
        attach.name = serverData.split("*sta*")[1];
        attach.url = serverData.split("*sta*")[2];
        AddCollectImg(attach);
    } catch (ex) {

    }
};

/* This is an example of how to cancel all the files queued up.  It's made somewhat generic.  Just pass your SWFUpload
object in to this method and it loops through cancelling the uploads. */
function cancelQueue(instance) {
	document.getElementById(instance.customSettings.cancelButtonId).disabled = true;
	instance.stopUpload();
	var stats;
	
	do {
		stats = instance.getStats();
		instance.cancelUpload();
	} while (stats.files_queued !== 0);
	
}

/* **********************
   Event Handlers
   These are my custom event handlers to make my
   web application behave the way I went when SWFUpload
   completes different tasks.  These aren't part of the SWFUpload
   package.  They are part of my application.  Without these none
   of the actions SWFUpload makes will show up in my application.
   ********************** */
function fileDialogStart() {
	/* I don't need to do anything here */
}
function fileQueued(file) {
	try {
		// You might include code here that prevents the form from being submitted while the upload is in
		// progress.  Then you'll want to put code in the Queue Complete handler to "unblock" the form
		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setStatus("进入队列..."); //Pending
		progress.toggleCancel(true, this);

	} catch (ex) {
		this.debug(ex);
	}

}

function fileQueueError(file, errorCode, message) {
	try {
		if (errorCode === SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED) {
			alert("现在队列里文件太多，请稍后重试.\n" + (message === 0 ? "您已经到达文件数量限制." : "您可以选择 " + (message > 1 ? "上传 " + message + " 个文件." : "一个文件.")));
			return;
		}

		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setError();
		progress.toggleCancel(false);

		switch (errorCode) {
		case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
			progress.setStatus("文件太大.");
			this.debug("错误代码: 文件太大, 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
			progress.setStatus("不能上传0字节的文件.");
			this.debug("错误代码: 0字节文件, 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
			progress.setStatus("无效的文件类型.");
			this.debug("错误代码: 无效的文件类型, 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
			alert("您已经选择了足够多的文件.  " +  (message > 1 ? "您最多可以添加 " +  message + " 个文件" : "您不能添加文件了."));
			break;
		default:
			if (file !== null) {
			    progress.setStatus("处理错误"); //Unhandled Error
			}
			this.debug("错误代码: " + errorCode + ", 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		}
	} catch (ex) {
        this.debug(ex);
    }
}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
	try {
		if (this.getStats().files_queued > 0) {
			document.getElementById(this.customSettings.cancelButtonId).disabled = false;
		}
		
		/* I want auto start and I can do that here */
		this.startUpload();
	} catch (ex)  {
        this.debug(ex);
	}
}

function uploadStart(file) {
	try {
		/* I don't want to do any file validation or anything,  I'll just update the UI and return true to indicate that the upload should start */
		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setStatus("上传中...");
		progress.toggleCancel(true, this);
	}
	catch (ex) {
	}
	
	return true;
}

function uploadProgress(file, bytesLoaded, bytesTotal) {

	try {
		var percent = Math.ceil((bytesLoaded / bytesTotal) * 100);

		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setProgress(percent);
		progress.setStatus("上传中...");
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadSuccess(file, serverData) {
    try {
        //alert(serverData);
		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setComplete();
		progress.setStatus("完成.");
		progress.toggleCancel(false);

	} catch (ex) {
		this.debug(ex);
	}
}

function uploadComplete(file) {
	try {
		/*  I want the next upload to continue automatically so I'll call startUpload here */
		if (this.getStats().files_queued === 0) {
			document.getElementById(this.customSettings.cancelButtonId).disabled = true;
		} else {	
			this.startUpload();
		}
	} catch (ex) {
		this.debug(ex);
	}

}

function uploadError(file, errorCode, message) {
	try {
		var progress = new FileProgress(file, this.customSettings.progressTarget);
		progress.setError();
		progress.toggleCancel(false);

		switch (errorCode) {
	    case SWFUpload.UPLOAD_ERROR.HTTP_ERROR:
			progress.setStatus("上传错误: " + message + ",可能已存在相同的文件.");
			this.debug("错误代码: HTTP 错误, 文件名: " + file.name + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.MISSING_UPLOAD_URL:
			progress.setStatus("配置错误");
			this.debug("错误代码: No backend file, 文件名: " + file.name + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_FAILED:
			progress.setStatus("上传失败.");
			this.debug("错误代码: 上传失败, 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.IO_ERROR:
			progress.setStatus("服务器IO读写错误");
			this.debug("错误代码: IO 错误, 文件名: " + file.name + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.SECURITY_ERROR:
		    progress.setStatus("服务器权限错误"); //Security Error
			this.debug("错误代码: 服务器权限错误, 文件名: " + file.name + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
		    progress.setStatus("超出了文件限制."); //Upload limit exceeded
			this.debug("错误代码: 超出了文件现在, 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.SPECIFIED_FILE_ID_NOT_FOUND:
			progress.setStatus("找不到文件.");
			this.debug("错误代码: 找不到文件, 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.FILE_VALIDATION_FAILED:
			progress.setStatus("文件验证失败. 已忽略.");
			this.debug("错误代码: 文件验证失败, 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
			if (this.getStats().files_queued === 0) {
				document.getElementById(this.customSettings.cancelButtonId).disabled = true;
			}
			progress.setStatus("Cancelled");
			progress.setCancelled();
			break;
		case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
			progress.setStatus("Stopped");
			break;
		default:
		    progress.setStatus("其他错误: " + error_code); //Unhandled Error
			this.debug("错误代码: " + errorCode + ", 文件名: " + file.name + ", 文件大小: " + file.size + ", 消息: " + message);
			break;
		}
	} catch (ex) {
        this.debug(ex);
    }
}