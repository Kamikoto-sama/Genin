import React, {useState} from 'react';
import {Col, ConfigProvider, Layout, Row, Space, theme} from 'antd';
import 'antd/dist/reset.css';
import ZoneSelect from "./components/ZoneSelect";
import ConfigsView from "./components/ConfigsView";
import ZoneCreateModal from "./components/ZoneCreateModal";

const App = () => {
    const [zone, setZone] = useState<string>();
    const [createZone, setCreateZone] = useState(false);

    return (
        <ConfigProvider theme={{algorithm: theme.darkAlgorithm}}>
            <ZoneCreateModal open={createZone} onClose={() => setCreateZone(false)}/>
            <Layout className="bg-none">
                <Space size="large" direction="vertical">
                    <Layout.Header>
                        <ZoneSelect onChange={setZone} onCreateZone={() => setCreateZone(true)}/>
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