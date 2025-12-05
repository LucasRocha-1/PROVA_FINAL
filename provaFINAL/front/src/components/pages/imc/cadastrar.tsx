import { useState } from "react";
import IMC from "../../../models/IMC";
import axios from "axios";

function CadastrarIMC() {
  //Estados
  const [nome, setNome] = useState("");
  const [altura, setAltura] = useState(0);
  const [peso, setPeso] = useState(0); 

    function enviarIMC(event: any) {
    event.preventDefault();
    submeterIMCAPI();
  }
  async function submeterIMCAPI() { 
    //Biblioteca AXIOS
    try {
      const imc: IMC = {
          nome, altura, peso,
          classificacao: "",
          imc: 0
      };
      const resposta = await axios.post("http://localhost:5011/api/imc/cadastrar", imc);            
      console.log(await resposta.data);
    } catch (error : any) {
        if(error.status === 409){
        console.log("Esse IMC já foi cadastrado!");
      }
    }
  }

  return (
    <div>
      <h1>Cadastrar IMC</h1>

  <form onSubmit={enviarIMC}>
        <div>
          <label>Nome:</label>
            <input onChange={(e : any) => setNome(e.target.value)} type="text" />
        </div>
        <div>
          <label>Altura (em metros):</label>
            <input onChange={(e : any) => setAltura(parseFloat(e.target.value))} type="number" step="0.01" />
        </div>
        <div>
          <label>Peso (em kg):</label>
            <input onChange={(e : any) => setPeso(parseFloat(e.target.value))} type="number" step="0.01" />
        </div>
        <button type="submit">Cadastrar</button>
      </form>
    </div>
    );
}

export default CadastrarIMC;