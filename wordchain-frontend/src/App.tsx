import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Form from './Form';


function App() {
  const [state, setState] = useState<{ source: string; target: string, chain: string[] | null, error: any }>({ source: '', target: '', chain: null, error: null });

  const handleSubmit = async (pair: { source: string; target: string }) => {
    let url = `https://${process.env.REACT_APP_API_URL}/wordchain`;
    let response = await fetch(url, {
      method: 'POST',
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(pair)
    });
    if (response.status === 400) {
      let text = await response.json();
      setState({ ...state, error: text, chain: null });
      return
    }

    if (!response.ok) {
      setState({ ...state, error: "Something went wrong!", chain: null });
    }
    let data = await response.json();
    setState({ ...state, error: null, chain: data });
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Word chain</h1>
        <Form onSubmit={handleSubmit} />
        <ul>
          {state.chain?.map((word) => (
            <li key={word}>
              {word}
            </li>
          ))}
        </ul>
        <div>
          {!!state.error && <label>{state.error}</label>}
        </div>
      </header>
    </div>
  );
}

export default App;
