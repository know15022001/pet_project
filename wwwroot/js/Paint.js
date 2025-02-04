initializeCanvas = (canvas) =>{
    const ctx = canvas.getContext('2d');
    let isMouseDown = false; // Biến để theo dõi trạng thái nhấn chuột
    canvas.addEventListener('mousedown', (e) => {
        ctx.beginPath();
        isMouseDown = true;
    });

    canvas.addEventListener('mousemove', (e) => {
        if (isMouseDown) {
            const rect = canvas.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            ctx.lineTo(x, y);
            ctx.stroke();
        }
    });

    canvas.addEventListener('mouseup', (e) => {
        isMouseDown = false;
    });
};

lineCanvas = (canvas) => {
    const ctx = canvas.getContext('2d');
    let isMouseDown = false; // Biến để theo dõi trạng thái nhấn chuột
    let startX, startY; // Biến để lưu trữ vị trí đầu nhấp chuột

    canvas.addEventListener('mousedown', (e) => {
        const rect = canvas.getBoundingClientRect();
        startX = e.clientX - rect.left;
        startY = e.clientY - rect.top;
        ctx.beginPath();
        ctx.moveTo(startX, startY);
        isMouseDown = true;
    });

    canvas.addEventListener('mousemove', (e) => {
        if (isMouseDown) {
            const rect = canvas.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            ctx.clearRect(0, 0, canvas.width, canvas.height); // xóa canvas
            ctx.beginPath(); // bắt đầu đường vẽ mới
            ctx.moveTo(startX, startY); // vị trí bắt đầu
            ctx.lineTo(x, y); // vẽ đến vị trí hiện tại
            ctx.stroke(); // Vẽ
        }
    });

    canvas.addEventListener('mouseup', (e) => {
        isMouseDown = false;
        ctx.closePath();
    });

    canvas.addEventListener('mouseleave', (e) => {
        isMouseDown = false; // Đánh dấu rằng chuột không còn được nhấn khi rời khỏi canvas
        ctx.closePath(); // Đóng đường vẽ
    });
}

changeColor = (canvas, color) => {
    const ctx = canvas.getContext('2d');
    ctx.strokeStyle = color;
};

getSignatureImage = (canvas) => {
    return canvas.toDataURL();
};

downloadFile = (dataURL, filename) => {
    // Convert base64 to Blob
    const byteString = atob(dataURL.split(',')[1]);
    const mimeString = dataURL.split(',')[0].split(':')[1].split(';')[0];
    const ab = new ArrayBuffer(byteString.length);
    const ia = new Uint8Array(ab);
    for (let i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([ab], { type: mimeString });

    // Create anchor element and trigger download
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = filename;
    link.click();
};

clearCanvas = (canvas) => {
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
};