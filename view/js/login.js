document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('loginForm');
    const roleTabs = document.querySelectorAll('.role-tab');
    const phoneInput = document.getElementById('phone');
    let currentRole = 'Customer';

    roleTabs.forEach(tab => {
        tab.addEventListener('click', () => {
            roleTabs.forEach(t => t.classList.remove('active'));
            tab.classList.add('active');
            currentRole = tab.dataset.role;
        });
    });

    // Chỉ cho phép nhập số, tối đa 10 ký tự
    if (phoneInput) {
        phoneInput.addEventListener('input', function () {
            this.value = this.value.replace(/[^0-9]/g, '').slice(0, 10);
        });
    }

    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const phone = phoneInput.value.trim();
        const password = document.getElementById('password').value;
        const loginError = document.getElementById('loginError');

        // Validate phía client
        if (phone.length !== 10 || !/^[0-9]{10}$/.test(phone)) {
            loginError.textContent = 'Số điện thoại phải bao gồm đúng 10 chữ số.';
            loginError.style.display = 'block';
            return;
        }

        try {
            const response = await fetch(`${API_BASE_URL}/api/Auth/login`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ phone, password, role: currentRole })
            });

            if (response.ok) {
                const data = await response.json();
                sessionStorage.setItem('isLoggedIn', 'true');
                sessionStorage.setItem('userPhone', phone);
                sessionStorage.setItem("userRole", currentRole);
                sessionStorage.setItem('fullName', data.fullName || 'Người dùng');
                
                if (data.workerProfileId) {
                    sessionStorage.setItem("workerProfileId", data.workerProfileId);
                }

                // ====== BẢO MẬT: Lưu JWT Token để gọi API bảo mật ======
                if (data.token) {
                    sessionStorage.setItem('authToken', data.token);
                }

                if (data.avatarUrl) {
                    const fullUrl = data.avatarUrl.startsWith("http") ? data.avatarUrl : (API_BASE_URL + data.avatarUrl);
                    sessionStorage.setItem('userAvatar', fullUrl);
                }

                if (currentRole === "Repairman") {
                    window.location.href = "worker-dashboard.html";
                } else {
                    window.location.href = "index.html";
                }
            } else {
                const errorText = await response.text();
                loginError.textContent = errorText || 'Số điện thoại hoặc mật khẩu không chính xác.';
                loginError.style.display = 'block';
            }
        } catch (error) {
            console.error(error);
            loginError.textContent = 'Lỗi kết nối máy chủ. Vui lòng thử lại sau.';
            loginError.style.display = 'block';
        }
    });
});
