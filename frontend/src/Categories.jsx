import { useEffect, useState } from "react";
import { api } from "./api.js";

export default function Categories() {
  const [categories, setCategories] = useState([]);
  const [name, setName] = useState("");
  const [error, setError] = useState("");

  async function load() {
    try { setError(""); setCategories(await api.getCategories()); }
    catch { setError("Could not reach the API. Is it running?"); }
  }

  useEffect(() => { load(); }, []);

  async function handleSubmit(e) {
    e.preventDefault();
    try { await api.createCategory({ name }); setName(""); load(); }
    catch { setError("Could not save the category."); }
  }

  return (
    <div>
      <h2>Categories</h2>
      {error && <p className="error">{error}</p>}
      <form className="form" onSubmit={handleSubmit}>
        <input placeholder="Name" value={name} onChange={(e) => setName(e.target.value)} required />
        <button type="submit">Add</button>
      </form>
      <ul className="list">
        {categories.map((c) => <li key={c.id}>{c.name}</li>)}
      </ul>
    </div>
  );
}