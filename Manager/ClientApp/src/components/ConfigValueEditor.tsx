import React from "react";
import CodeMirror from '@uiw/react-codemirror/';
import {json} from "@codemirror/lang-json";
import {Extension} from '@codemirror/state';
import {Alert, Space} from "antd";

export type ConfigValueFormat = 'Plain text' | 'JSON' | 'YAML';

function getFormatter(format: ConfigValueFormat): Extension[] {
    switch (format) {
        case "JSON":
            return [json()]
    }
    return []
}

function ConfigValueEditor({format, value, onChange, error, onErrorClose}: Props) {
    const formatter = getFormatter(format)
    return (
        <Space direction="vertical" size="small" className="full-width">
            <CodeMirror
                theme="dark"
                placeholder={`Enter ${format} value (can be empty)`}
                extensions={formatter}
                value={value}
                onChange={onChange}
            />
            {
                error &&
				<Alert
					type="error"
					showIcon
					message={error}
					closable={true}
					onClose={onErrorClose}
				/>
            }
        </Space>
    )
}

interface Props {
    format: ConfigValueFormat;
    value: string;
    onChange: (newValue: string) => void;
    error?: string;
    onErrorClose: () => any;
}

export default ConfigValueEditor;