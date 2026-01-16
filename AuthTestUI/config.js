const CONFIG = {
    // قم بتغيير المنفذ (Port) حسب اللي شغال عندك ف الـ Visual Studio (عادة 5000 أو 7000+)
    BASE_URL: 'https://localhost:7002/Api/V1/Auth'
};

async function handleResponse(response) {
    const text = await response.text();
    const data = text ? JSON.parse(text) : {};

    if (!response.ok) {
        throw new Error(data.message || 'Something went wrong');
    }
    return data;
}

function showAlert(type, message) {
    const alert = document.getElementById('alert');
    alert.textContent = message;
    alert.className = `alert alert-${type}`;
    alert.style.display = 'block';
}

function toggleLoading(isLoading) {
    const btn = document.querySelector('button');
    const spinner = document.querySelector('.spinner');
    if (isLoading) {
        btn.disabled = true;
        spinner.style.display = 'inline-block';
    } else {
        btn.disabled = false;
        spinner.style.display = 'none';
    }
}
