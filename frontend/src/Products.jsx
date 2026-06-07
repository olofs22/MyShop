import { useEffect, useState } from "react";
import { api } from "./api.js";

export default function Products() {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [error, setError] = useState("");
  const [form, setForm] = useState({ name: "", price: "", categoryId: "" });
  const [editingId, setEditingId] = useState(null);

  async function load() {
    try {
      setError("");
      setProducts(await api.getProducts());
      setCategories(await api.getCategories());
    } catch {
      setError("Could not reach the API. Is it running?");
    }
  }

  useEffect(() => { load(); }, []);

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      const data = {
        name: form.name,
        price: parseFloat(form.price),
        categoryId: parseInt(form.categoryId),
      };
      if (editingId) await api.updateProduct(editingId, data);
      else await api.createProduct(data);
      setForm({ name: "", price: "", categoryId: "" });
      setEditingId(null);
      load();
    } catch {
      setError("Could not save the product.");
    }
  }

  function startEdit(p) {
    setEditingId(p.id);
    setForm({ name: p.name, price: p.price, categoryId: p.categoryId });
  }

  async function handleDelete(id) {
    try { await api.deleteProduct(id); load(); }
    catch { setError("Could not delete the product."); }
  }

  return (
    <div>
      <h2>Products</h2>
      {error && <p className="error">{error}</p>}

      <form className="form" onSubmit={handleSubmit}>
        <input placeholder="Name" value={form.name}
               onChange={(e) => setForm({ ...form, name: e.target.value })} required />
        <input placeholder="Price" type="number" value={form.price}
               onChange={(e) => setForm({ ...form, price: e.target.value })} required />
        <select value={form.categoryId}
                onChange={(e) => setForm({ ...form, categoryId: e.target.value })} required>
          <option value="">Choose category</option>
          {categories.map((c) => <option key={c.id} value={c.id}>{c.name}</option>)}
        </select>
        <button type="submit">{editingId ? "Update" : "Add"}</button>
        {editingId && (
          <button type="button" className="secondary"
                  onClick={() => { setEditingId(null); setForm({ name: "", price: "", categoryId: "" }); }}>
            Cancel
          </button>
        )}
      </form>

      <ul className="list">
        {products.map((p) => (
          <li key={p.id}>
            <span>{p.name} <span className="price">{p.price} kr · {p.categoryName}</span></span>
            <span className="item-actions">
              <button className="small secondary" onClick={() => startEdit(p)}>Edit</button>
              <button className="small danger" onClick={() => handleDelete(p.id)}>Delete</button>
            </span>
          </li>
        ))}
      </ul>
    </div>
  );
}