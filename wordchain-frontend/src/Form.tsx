import React, { useState, FormEvent } from 'react';

interface FormProps {
    onSubmit: (pair: { source: string; target: string }) => void;
}

const Form: React.FC<FormProps> = ({ onSubmit }) => {
    const [source, setSourceWord] = useState('');
    const [target, setTargetWord] = useState('');

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        if (!source.trim() || !target.trim()) {
            alert('Please fill in both source and target words.');
            return;
        }
        onSubmit({ source: source, target: target });
        setSourceWord('');
        setTargetWord('');
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Source Word:
                <input
                    type="text"
                    value={source}
                    onChange={(e) => setSourceWord(e.target.value)}
                />
            </label>
            <br />
            <label>
                Target Word:
                <input
                    type="text"
                    value={target}
                    onChange={(e) => setTargetWord(e.target.value)}
                />
            </label>
            <br />
            <button type="submit">Submit</button>
        </form>
    );
};

export default Form;