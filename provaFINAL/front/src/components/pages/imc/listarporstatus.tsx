import { useEffect,useState } from "react";
import IMC from "../../../models/IMC";
import axios from "axios";

function ListarPorStatus() {
    const [imcs, setImcs] = useState<IMC[]>([]);
    useEffect(() => {
    console.log("O componente foi carregado!");
    buscarIMCAPI();
  }, []);

  async function buscarIMCAPI() {
    try {
      const resposta = await axios.get(
        "http://localhost:5078/api/imc/listarporstatus"
      );
      setImcs(resposta.data);
    } catch (error) {
      console.log("Erro na requisição: " + error);
    }
  }

  return (
    <div>
      <h1>IMCs por Status</h1>
      <ul>
        {imcs.map((imc) => (
          <li key={imc.id}>
            {imc.classificacao} - {imc.imc}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ListarPorStatus;