test_sents = (
    "Ә чынында исә, кеше түгел, гөмбә ул!",
    "Андагы пулемет һәм автомат тавышлары монда бик тонык ишетелә.",
    "Озак та үтмәде, күбәләк-күбәләк кар ява башлады.",
    "Күк йөзе, авыр болытлардан әкрен-әкрен генә арчылып, зәңгәр күл сыман, урман өстенә җәелде.",
    "Меңнәрчә, миллионнарча кар энҗеләре җем-җем итеп балкыйлар.",
    "Барыбыз да ул күрсәткән якка борылдык.",
    "Шуннан соң без аның турында һичнәрсә ишетмәдек.",
    "Аның тавышында мин бүгенгә кадәр һич тә ишетмәгән әллә нинди сагыш, әрнү сиздем һәм үземнең бу саксыз-лыгым өчен аңардан гафу үтендем.",
    "Дөресен әйткәндә, сез икенче тапкыр дөньяга тудыгыз.",
    "Ә Хәйбулланың үле гәүдәсе дә юк, бары тик бүреге һәм каскасы гына төшеп калган иде.",
    "Сугышчылар, коралларына тотынып, тын да алмыйча көтә башладылар.",
)


for i, test_sent in enumerate(test_sents):
    test_sequence = normalize_string(test_sent)
    predict(test_sequence)
    
    en_alignments, de_bot_alignments, de_mid_alignments, source, prediction = predict()
    
    attention = de_mid_alignments[3][0, 2, :].numpy()
print(attention.shape)

fig = plt.figure(figsize=(10, 10))
ax = fig.add_subplot(1, 1, 1)
ax.matshow(attention, cmap='jet')
ax.set_xticklabels([''] + source, rotation=90)
ax.set_yticklabels([''] + prediction)
