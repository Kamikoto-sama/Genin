import React, {useState} from 'react';
import {Col, ConfigProvider, Layout, Row, Space, theme} from 'antd';
import 'antd/dist/reset.css';
import ZoneSelect from "./components/ZoneSelect";
import ConfigsView from "./components/ConfigsView";

const App = () => {
    const [zone, setZone] = useState<string>();

    return (
        <ConfigProvider theme={{algorithm: theme.darkAlgorithm}}>
            <Layout className="bg-none">
                <Space size="large" direction="vertical">
                    <Layout.Header>
                        <ZoneSelect onSelect={setZone}/>
                    </Layout.Header>
                    <Layout.Content>
                        <Row>
                            <Col offset={4} span={16}>
                                <ConfigsView zone={zone}/>
                            </Col>
                        </Row>
                    </Layout.Content>
                </Space>
            </Layout>
        </ConfigProvider>
    )
};

export default App;