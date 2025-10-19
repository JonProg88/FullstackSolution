import { useEffect, useState } from "react";
import { api } from "../services/api";
import ErrorBanner from "../components/ErrorBanner";

type Product = { id:number; name:string; price:number; stock:number; createdAt:string };
type Paginated = { total:number; page:number; size:number; items: Product[] };

export default function ProductsList(){
  const [data,setData] = useState<Paginated | null>(null);
  const [loading,setLoading] = useState(true);
  const [error,setError] = useState("");

  const load = async (page=1, size=10) => {
    setLoading(true); setError("");
    try {
      const r = await api.get(`/products?page=${page}&size=${size}`);
      setData(r.data);
    } catch {
      setError("No se pudo cargar la lista.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(()=>{ load(1, 20); },[]);

  if (loading) return <p>Cargando…</p>;
  if (error) return <ErrorBanner message={error} />;
  if (!data) return <p>Vacío.</p>;

  return (
    <section>
      <div className="card">
        <h2>Listado</h2>
        <p>Total: {data.total}</p>
      </div>

      <ul className="list">
        {data.items.map(p => (
          <li key={p.id} className="card">
            <strong>{p.name}</strong><br />
            ${p.price} — stock {p.stock}<br/>
            <small>{new Date(p.createdAt).toLocaleString()}</small>
          </li>
        ))}
      </ul>
    </section>
  );
}
