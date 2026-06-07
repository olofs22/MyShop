
const BASE_URL = "http://localhost:5083/api";

async function request(path, options = {}) {
  const res = await fetch(`${BASE_URL}${path}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });
  if (!res.ok) throw new Error(`API error: ${res.status}`);
  if (res.status === 204) return null; 
  return res.json();
}

export const api = {
  getProducts: () => request("/products"),
  createProduct: (d) => request("/products", { method: "POST", body: JSON.stringify(d) }),
  updateProduct: (id, d) => request(`/products/${id}`, { method: "PUT", body: JSON.stringify(d) }),
  deleteProduct: (id) => request(`/products/${id}`, { method: "DELETE" }),
  getCategories: () => request("/categories"),
  createCategory: (d) => request("/categories", { method: "POST", body: JSON.stringify(d) }),
};