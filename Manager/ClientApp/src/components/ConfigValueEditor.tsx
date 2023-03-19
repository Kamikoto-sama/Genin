import React from "react";
import CodeMirror from '@uiw/react-codemirror/';
import {json} from "@codemirror/lang-json";
import {Extension} from '@codemirror/state';

export type ConfigValueFormat = 'Plain text' | 'JSON' | 'YAML';

function getFormatter(format: ConfigValueFormat): Extension[] {
    switch (format) {
        case "JSON":
            return [json()]
    }
    return []
}

function ConfigValueEditor({format, value, onChange}: Props) {
    const formatter = getFormatter(format)
    return (
        <CodeMirror
            theme="dark"
            placeholder={`Enter ${format} value (can be empty)`}
            extensions={formatter}
            value={value}
            onChange={onChange}
        />
    )
}

interface Props {
    format: ConfigValueFormat;
    value: string;
    onChange: (newValue: string) => void;
}

export default ConfigValueEditor;