const Style = (theme) => ({
    root: {
         width: '90%',
    },
    backButton: {
        marginRight: theme.spacing.unit,
    },
    instructions: {
        marginTop: theme.spacing.unit,
        marginBottom: theme.spacing.unit,
    },
    background: {
         backgroundColor: 'transparent'
    },
    location: {
        display: 'flex', 
        justifyContent:'flex-end'
    },
});


export default Style;