const API_BASE = "/api";
const tokenKey = "qlkh_token";

const state = {
  token: localStorage.getItem(tokenKey) || "",
};

const loginForm = document.querySelector("#loginForm");
const registerForm = document.querySelector("#registerForm");
const courseForm = document.querySelector("#courseForm");
const courseList = document.querySelector("#courseList");
const totalCourses = document.querySelector("#totalCourses");
const toast = document.querySelector("#toast");
const logoutBtn = document.querySelector("#logoutBtn");
const refreshCoursesBtn = document.querySelector("#refreshCoursesBtn");

function showToast(message) {
  toast.textContent = message;
  toast.classList.add("show");
  setTimeout(() => toast.classList.remove("show"), 2200);
}

function updateNav() {
  const navButtons = document.querySelectorAll(".nav-item");
  navButtons.forEach((button) => {
    button.addEventListener("click", () => {
      navButtons.forEach((item) => item.classList.remove("active"));
      button.classList.add("active");

      const target = button.dataset.section;
      document.querySelectorAll(".section").forEach((section) => {
        section.classList.toggle("active-section", section.id === target);
      });
    });
  });
}

async function requestJson(url, options = {}) {
  const headers = {
    "Content-Type": "application/json",
    ...(options.headers || {}),
  };

  if (state.token) {
    headers.Authorization = `Bearer ${state.token}`;
  }

  const response = await fetch(url, {
    ...options,
    headers,
  });

  const text = await response.text();
  const payload = text ? JSON.parse(text) : null;

  if (!response.ok) {
    throw new Error(payload?.message || payload?.title || "Yêu cầu API thất bại");
  }

  return payload;
}

async function loginUser(event) {
  event.preventDefault();

  const email = document.querySelector("#loginEmail").value.trim();
  const password = document.querySelector("#loginPassword").value.trim();

  try {
    const data = await requestJson(`${API_BASE}/Auth/login`, {
      method: "POST",
      body: JSON.stringify({ email, password }),
    });

    state.token = data.token;
    localStorage.setItem(tokenKey, state.token);
    showToast("Đăng nhập thành công");
    document.querySelector("#loginForm").reset();
    await loadCourses();
  } catch (error) {
    showToast(error.message || "Đăng nhập thất bại");
  }
}

async function registerUser(event) {
  event.preventDefault();

  const fullName = document.querySelector("#registerName").value.trim();
  const email = document.querySelector("#registerEmail").value.trim();
  const password = document.querySelector("#registerPassword").value.trim();

  try {
    await requestJson(`${API_BASE}/Auth/register`, {
      method: "POST",
      body: JSON.stringify({ fullName, email, password }),
    });

    showToast("Tạo tài khoản thành công");
    document.querySelector("#registerForm").reset();
  } catch (error) {
    showToast(error.message || "Tạo tài khoản thất bại");
  }
}

function renderCourses(items) {
  totalCourses.textContent = items.length;

  if (!items.length) {
    courseList.innerHTML = "<p>Chưa có khóa học nào.</p>";
    return;
  }

  courseList.innerHTML = items
    .map(
      (course) => `
        <article class="course-item">
          <div class="course-meta">
            <h4>${course.title}</h4>
            <p>${course.description || "Không có mô tả"}</p>
            <span class="price-tag">${new Intl.NumberFormat("vi-VN", {
              style: "currency",
              currency: "VND",
            }).format(course.price || 0)}</span>
          </div>
          <span>${course.isPublished ? "Đã xuất bản" : "Nháp"}</span>
        </article>
      `
    )
    .join("");
}

async function loadCourses() {
  try {
    const data = await requestJson(`${API_BASE}/Courses`);
    renderCourses(Array.isArray(data) ? data : []);
  } catch (error) {
    showToast(error.message || "Không thể tải khóa học");
    renderCourses([]);
  }
}

async function createCourse(event) {
  event.preventDefault();

  const title = document.querySelector("#courseTitle").value.trim();
  const description = document.querySelector("#courseDescription").value.trim();
  const category = document.querySelector("#courseCategory").value.trim();
  const price = Number(document.querySelector("#coursePrice").value);

  try {
    await requestJson(`${API_BASE}/Courses`, {
      method: "POST",
      body: JSON.stringify({ title, description, price, categorySlug: category }),
    });

    showToast("Thêm khóa học thành công");
    courseForm.reset();
    await loadCourses();
  } catch (error) {
    showToast(error.message || "Thêm khóa học thất bại");
  }
}

function logout() {
  state.token = "";
  localStorage.removeItem(tokenKey);
  showToast("Đăng xuất thành công");
}

loginForm.addEventListener("submit", loginUser);
registerForm.addEventListener("submit", registerUser);
courseForm.addEventListener("submit", createCourse);
logoutBtn.addEventListener("click", logout);
refreshCoursesBtn.addEventListener("click", loadCourses);

updateNav();
loadCourses();
